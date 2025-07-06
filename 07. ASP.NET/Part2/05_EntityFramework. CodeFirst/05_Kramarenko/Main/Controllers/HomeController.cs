using Main.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
	public class HomeController : Controller
	{
		private readonly ApplicationContext _context;
		IWebHostEnvironment _hostingEnvironment;

		public HomeController(IWebHostEnvironment hostingEnvironment, ApplicationContext context)
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
			List<Category> categories = _context.Categories.ToList();

			PicturesViewModel viewModel = new PicturesViewModel()
			{
				Categories = categories,
				CategoryID = -1
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult GetPhotosByType(int? typeID)
		{
			List<Picture> pictures = null;

			if (!typeID.HasValue || typeID == 0) pictures = _context.Pictures.Include(x => x.Category).ToList();
			else pictures = _context.Pictures.Where(x => x.CategoryID == typeID).Include(x => x.Category).ToList();

			return Json(pictures.Select(x => new {
				id = x.ID,
				imgSrc = x.ImgSrc,
				caption = x.Caption,
				description = x.Description,
				category = x.Category.Value,
				pubDate = x.CreationDate.ToString(@"dd.MM.yyyy")
			}));
		}

		[HttpPost]
		public IActionResult Delete(int? id)
		{
			try
			{
				if (!id.HasValue) throw new Exception();

				var pic = _context.Pictures.Where(x => x.ID == id).FirstOrDefault();
				if (pic != null)
				{
					_context.Pictures.Remove(pic);
					_context.SaveChanges();
				}
				DirectoryInfo dInfo = new DirectoryInfo($"{_hostingEnvironment.ContentRootPath}/wwwroot/Images");
				var files = dInfo.GetFiles($"{id}.*");

				if (files.Length != 1) throw new Exception();
				files[0].Delete();

				return Json(true);
			}
			catch
			{
				return Json(false);
			}
		}

		[HttpPost]
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
				IFormFile file = files[0];
				Picture picture = new Picture()
				{
					Caption = caption,
					CategoryID = categoryID.Value,
					CreationDate = DateTime.Now,
					Description = description,
					ImgSrc = "/"
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


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
