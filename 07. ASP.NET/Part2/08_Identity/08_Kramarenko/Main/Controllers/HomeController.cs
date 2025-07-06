using Main.Areas.Identity.Data;
using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private readonly ApplicationDBContext _context;
		IWebHostEnvironment _hostingEnvironment;

		public HomeController(
			IWebHostEnvironment hostingEnvironment, 
			UserManager<ApplicationUser> userManager,
			ApplicationDBContext context)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Pictures()
		{			
			List<Picture> pictures = _context.Pictures.ToList();
			List<Category> categories = _context.Categories.ToList();

			PicturesViewModel viewModel = new PicturesViewModel()
			{
				Pictures = pictures,
				Categories = categories,
				CategoryID = 0,
				IsAdmin = User.IsInRole("Admin")
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult GetPhotosByType(int? typeID)
		{
			List<Picture> pictures = null;

			if (!typeID.HasValue || typeID == 0) pictures = _context.Pictures.Include(x => x.Category).ToList();
			else if (typeID == -1 && User.Identity.IsAuthenticated)
			{
				string myID = GetMyID();
				pictures = _context.Pictures.Where(x => x.UserID == myID).Include(x => x.Category).ToList();
			}
			else pictures = _context.Pictures.Where(x => x.CategoryID == typeID).Include(x => x.Category).ToList();

			return Json(pictures.Select(x => new
			{
				id = x.ID,
				imgSrc = x.ImgSrc,
				caption = x.Caption,
				description = x.Description,
				category = x.Category.Value,
				pubDate = x.CreationDate.ToString(@"dd.MM.yyyy"),
				canDelete = typeID == -1 || User.IsInRole("Admin")
			}));
		}

		[HttpPost]
		[Authorize]
		public IActionResult Delete(int? id)
		{
			try
			{
				if (!id.HasValue) throw new Exception();

				var pic = _context.Pictures.Where(x => x.ID == id).Include(x => x.Comments).FirstOrDefault();
				bool flag = false;
				if (pic != null)
				{
					var user = _context.Users.Where(x => x.UserName == User.Identity.Name).Include(x => x.Pictures).FirstOrDefault();

					if (User.IsInRole("Admin") || user.Pictures.Contains(pic))
					{
						flag = true;
						if (pic.Comments.Count > 0) 
							_context.Comments.RemoveRange(pic.Comments);
						_context.Pictures.Remove(pic);
						_context.SaveChanges();
					}
				}
				if (flag)
				{
					DirectoryInfo dInfo = new DirectoryInfo($"{_hostingEnvironment.ContentRootPath}/wwwroot/Images");
					var files = dInfo.GetFiles($"{id}.*");

					if (files.Length != 1) throw new Exception();
					files[0].Delete();

					return Json(true);
				}
			}
			catch { }
			return Json(false);
		}

		[HttpPost]
		[Authorize]
		public IActionResult AddPhoto(IFormFileCollection files, string caption, string description, int? categoryID, int? myCategoryID)
		{
			caption = caption?.Trim(' ', '\t', '\n') ?? "";
			description = description?.Trim(' ', '\t', '\n') ?? "";

			var categoryIDs = _context.Categories.Select(x => x.ID);
			if (files == null || files.Count != 1 || files[0] == null ||
				caption.Length < 3 || !categoryID.HasValue || !myCategoryID.HasValue ||
				(!categoryIDs.Contains(categoryID.Value) &&
				(myCategoryID != 0 && !categoryIDs.Contains(myCategoryID.Value))))
				return Json(new { isError = true });

			try
			{
				string myID = GetMyID();
				IFormFile file = files[0];
				Picture picture = new Picture()
				{
					Caption = caption,
					CategoryID = categoryID.Value,
					CreationDate = DateTime.Now,
					Description = description,
					ImgSrc = "/", 
					UserID = myID
				};

				_context.Pictures.Add(picture);
				_context.SaveChanges();

				string shortPath = $"/Images/{picture.ID}{System.IO.Path.GetExtension(file.FileName)}";
				string fullPath = $"{_hostingEnvironment.WebRootPath}{shortPath}";

				using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
				{
					file.CopyTo(fileStream);
				}

				picture.ImgSrc = shortPath;
				_context.SaveChanges();
				picture = _context.Pictures.Where(x => x.ID == picture.ID).Include(x => x.Category).FirstOrDefault();

				bool isPass = myCategoryID == 0 || myCategoryID == categoryID;
				return Json(new
				{
					isPass = isPass,
					image = !isPass ? null :
						new
						{
							id = picture.ID,
							imgSrc = picture.ImgSrc,
							caption = picture.Caption,
							description = picture.Description,
							category = picture.Category.Value,
							pubDate = picture.CreationDate.ToString(@"dd.MM.yyyy")
						}
				});
			}
			catch { }

			return Json(false);
		}

		public IActionResult DetailPicture(int? id)
		{
			if (!id.HasValue) return RedirectToAction("Pictures");

			var pics = _context.Pictures.Where(x => x.ID == id).Include(x => x.Category).Include(x => x.Comments);
			if (pics.Count() != 1) return RedirectToAction("Pictures");

			Picture pic = pics.First();
			pic.Comments = pic.Comments.OrderByDescending(x => x.Date).ToList();
			return View(pic);
		}

		[HttpPost]
		[Authorize]
		public IActionResult AddComment(int? pictureID, string msg)
		{
			if (!pictureID.HasValue || msg == null) return Json(null);
			msg = msg.Trim(' ', '\t', '\n');
			if (msg.Length == 0) return Json(null);

			var pictures = _context.Pictures.Where(x => x.ID == pictureID);
			if (pictures.Count() != 1) return Json(null);

			try
			{
				string myID = GetMyID();
				Picture picture = pictures.First();
				Comment comment = new Comment()
				{
					Date = DateTime.Now,
					Content = msg,
					PictureID = pictureID.Value,
					UserID = myID
				};
				_context.Comments.Add(comment);
				_context.SaveChanges();

				msg = msg.Replace("\n", "<br>");
				return Json(new
				{
					isOk = true,
					comment = new
					{
						date = comment.Date.ToString(@"dd.MM.yyyy HH:mm"),
						msg = msg
					}
				});
			}
			catch { }

			return Json(false);
		}

		private string GetMyID()
		{
			if (!User.Identity.IsAuthenticated) return "";
			return _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault()?.Id ?? "";
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
