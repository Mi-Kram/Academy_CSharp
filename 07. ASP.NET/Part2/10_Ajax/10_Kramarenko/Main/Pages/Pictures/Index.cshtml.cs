using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Main.Pages.Pictures
{
    public class IndexModel : PageModel
    {
		private readonly ApplicationDBContext _context;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public List<Picture> Pictures { get; set; } = new List<Picture>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public int CategoryID { get; set; } = 0;
        public bool IsAdmin { get; set; } = false;

		public int OnePageCount { get; set; } = 3;
		public int CurrentPage { get; set; } = 1;

		public IndexModel(ApplicationDBContext context, IWebHostEnvironment hostingEnvironment)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;
		}

        public async Task OnGetAsync()
        {
			Pictures = await _context.Pictures.ToListAsync();
			Categories = await _context.Categories.ToListAsync();
			CategoryID = 0;
			IsAdmin = User.IsInRole("Admin");
		}

		public async Task<JsonResult> OnPostPageAsync(int? page, int? type)
		{
			if (!page.HasValue || !type.HasValue) return new JsonResult(null);
			if (type == -1 && !User.Identity.IsAuthenticated) type = 0;
			CurrentPage = page.Value;

			if (type == -1)
			{
				string myID = await GetMyIDAsync();
				Pictures = await _context.Pictures.Where(x => x.UserID == myID).Skip((CurrentPage - 1) * OnePageCount).Take(OnePageCount).ToListAsync();
			}
			else if (type == 0) Pictures = await _context.Pictures.Skip((CurrentPage - 1) * OnePageCount).Take(OnePageCount).ToListAsync();
			else Pictures = await _context.Pictures.Where(x => x.CategoryID == type).Skip((CurrentPage - 1) * OnePageCount).Take(OnePageCount).ToListAsync();
			
			return new JsonResult(Pictures.Select(x => new
			{
				id = x.ID,
				imgSrc = x.ImgSrc,
				canDelete = type.Value == -1
			}));

		}

		public async Task<JsonResult> OnPostByTypeAsync(int? typeID)
		{
			if (!typeID.HasValue) Pictures.Clear();
			else if (typeID == 0) Pictures = await _context.Pictures.ToListAsync();
			else if (typeID == -1 && User.Identity.IsAuthenticated)
			{
				string myID = await GetMyIDAsync();
				Pictures = await _context.Pictures.Where(x => x.UserID == myID).ToListAsync();
			}
			else Pictures = await _context.Pictures.Where(x => x.CategoryID == typeID).ToListAsync();

			int paggingCount = Pictures.Count / OnePageCount;
			if (Pictures.Count % OnePageCount != 0) paggingCount++;

			Pictures = Pictures.Take(OnePageCount).ToList();

			return new JsonResult(new 
			{
				pictures = Pictures.Select(x => new
				{
					id = x.ID,
					imgSrc = x.ImgSrc,
					canDelete = typeID == -1 || User.IsInRole("Admin")
				}),
				paggingCount = paggingCount,
				href = "/Pictures/Index/Page?page="
			});
		}

		public async Task<JsonResult> OnPostDeleteAsync(int? id)
		{
			try
			{
				if (!id.HasValue) throw new Exception();

				var pic = await _context.Pictures.Where(x => x.ID == id).Include(x => x.Comments).FirstOrDefaultAsync();
				bool flag = false;
				if (pic != null)
				{
					var user = await _context.Users.Where(x => x.UserName == User.Identity.Name).Include(x => x.Pictures).FirstOrDefaultAsync();

					if (User.IsInRole("Admin") || user.Pictures.Contains(pic))
					{
						flag = true;
						if (pic.Comments.Count > 0)
							_context.Comments.RemoveRange(pic.Comments);
						_context.Pictures.Remove(pic);
						await _context.SaveChangesAsync();
					}
				}
				if (flag)
				{
					DirectoryInfo dInfo = new DirectoryInfo($"{_hostingEnvironment.ContentRootPath}/wwwroot/Images");
					var files = dInfo.GetFiles($"{id}.*");

					if (files.Length != 1) throw new Exception();
					files[0].Delete();

					return new JsonResult(true);
				}
			}
			catch { }
			return new JsonResult(false);
		}

		public async Task<JsonResult> OnPostAddPhotoAsync(IFormFileCollection files, string caption, string description, int? categoryID, int? myCategoryID)
		{
			caption = caption?.Trim(' ', '\t', '\n') ?? "";
			description = description?.Trim(' ', '\t', '\n') ?? "";

			var categoryIDs = _context.Categories.Select(x => x.ID);
			if (files == null || files.Count != 1 || files[0] == null ||
				caption.Length < 3 || !categoryID.HasValue || !myCategoryID.HasValue ||
				(!categoryIDs.Contains(categoryID.Value) &&
				(myCategoryID != 0 && !categoryIDs.Contains(myCategoryID.Value))))
				return new JsonResult(new { isError = true });

			try
			{
				string myID = await GetMyIDAsync();
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
				await _context.SaveChangesAsync();

				string shortPath = $"/Images/{picture.ID}{System.IO.Path.GetExtension(file.FileName)}";
				string fullPath = $"{_hostingEnvironment.WebRootPath}{shortPath}";

				using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
				{
					file.CopyTo(fileStream);
				}

				picture.ImgSrc = shortPath;
				await _context.SaveChangesAsync();
				picture = await _context.Pictures.Where(x => x.ID == picture.ID).Include(x => x.Category).FirstOrDefaultAsync();

				bool isPass = myCategoryID == 0 || myCategoryID == categoryID;
				return new JsonResult(new
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

			return new JsonResult(false);
		}

		private async Task<string> GetMyIDAsync()
		{
			if (!User.Identity.IsAuthenticated) return "";
			return (await _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefaultAsync())?.Id ?? "";
		}
	}
}
