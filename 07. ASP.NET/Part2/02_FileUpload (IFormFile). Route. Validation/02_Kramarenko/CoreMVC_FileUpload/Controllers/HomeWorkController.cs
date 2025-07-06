using CoreMVC_FileUpload.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_FileUpload.Controllers
{
	public class HomeWorkController : Controller
	{
		IWebHostEnvironment _hostingEnvironment;

		public HomeWorkController(IWebHostEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Index()
		{
			List<ImageModel> imgSrcs = new List<ImageModel>();
			DirectoryInfo dInfo = new DirectoryInfo($"{_hostingEnvironment.ContentRootPath}/wwwroot/Images");
			DirectoryInfo[] imgTypes = dInfo.GetDirectories();

			foreach (DirectoryInfo dir in imgTypes)
			{
				imgSrcs.AddRange(dir.GetFiles().Select(x => new ImageModel()
				{
					ID = System.IO.Path.GetFileNameWithoutExtension(x.Name),
					Path = $"/Images/{dir.Name}/{x.Name}"
				}));
			}

			return View(imgSrcs);
		}

		[HttpPost]
		public IActionResult GetPhotosByType(string type = "")
		{
			if (type == "") return Json(new { url=Url.Action("Index") });

			List<ImageModel> imgSrcs = new List<ImageModel>();
			if (type == "Все")
			{
				DirectoryInfo dInfo = new DirectoryInfo($"{_hostingEnvironment.ContentRootPath}/wwwroot/Images");
				DirectoryInfo[] imgTypes = dInfo.GetDirectories();

				foreach (DirectoryInfo dir in imgTypes)
				{
					imgSrcs.AddRange(dir.GetFiles().Select(x => new ImageModel()
					{
						ID = System.IO.Path.GetFileNameWithoutExtension(x.Name),
						Path = $"/Images/{dir.Name}/{x.Name}"
					}));
				}
			}
			else
			{
				DirectoryInfo dInfo = new DirectoryInfo($"{_hostingEnvironment.ContentRootPath}/wwwroot/Images/{type}");
				if (!dInfo.Exists) return Json(new { url = Url.Action("Index") });

				DirectoryInfo[] imgTypes = dInfo.GetDirectories();
				imgSrcs.AddRange(dInfo.GetFiles().Select(x => new ImageModel()
				{
					ID = System.IO.Path.GetFileNameWithoutExtension(x.Name),
					Path = $"/Images/{dInfo.Name}/{x.Name}"
				}));
			}

			return Json(imgSrcs.ToArray());
		}

		[HttpPost]
		public IActionResult Delete(int? id)
		{
			try
			{
				if (!id.HasValue) throw new Exception();

				DirectoryInfo dInfo = new DirectoryInfo($"{_hostingEnvironment.ContentRootPath}/wwwroot/Images");
				var files = dInfo.GetFiles($"{id}.*", SearchOption.AllDirectories);

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
		public IActionResult DownloadImages(ImageFileModel model)
		{
			string dirPath = $"{_hostingEnvironment.ContentRootPath}/wwwroot/Images/{model.image_type}";
			if (!Directory.Exists(dirPath))
				return Json(false);

			DirectoryInfo dInfo = new DirectoryInfo($"{_hostingEnvironment.ContentRootPath}/wwwroot/Images");
			List<int> ids = dInfo.GetFiles("*.*", SearchOption.AllDirectories).Select(x => int.Parse(System.IO.Path.GetFileNameWithoutExtension(x.Name))).OrderBy(x => x).ToList();
			List<ImageModel> newImages = new List<ImageModel>();
			int currentID = 1;

			foreach (IFormFile file in model.files)
			{
				// проверка наличия файла
				if (file != null)
				{
					while (ids.Contains(currentID)) currentID++;
					var savePath = $"{dirPath}/{currentID}{System.IO.Path.GetExtension(file.FileName)}";
					using (var fileStream = new FileStream(savePath, FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					newImages.Add(new ImageModel()
					{
						ID = currentID.ToString(),
						Path = $"/Images/{model.image_type}/{currentID++}{System.IO.Path.GetExtension(file.FileName)}"
					});
				}
			}

			return Json(new {
				isPass = model.image_type == model.current_type || model.current_type == "Все",
				images = newImages
			});
		}
	}
}
