using CoreMVC_FileUpload.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_FileUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IWebHostEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home  
        public ActionResult UploadFiles()
        {
            return View();
        }

        public ActionResult AjaxUploadFiles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadFiles(FileModel model)
        {
            // проверка корректности валидации
            if (ModelState.IsValid)
            {
                // перебрать коллекцию полученных файлов
                foreach (IFormFile file in model.files)
                {
                    // проверка наличия файла
                    if (file != null)
                    {
                        var savePath = _hostingEnvironment.WebRootPath + "/Files/" + file.FileName;

                        // сохранение файла
                        using (var fileStream = new FileStream(savePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // установка сообщения о загрузке файлов
                        ViewBag.UploadStatus = model.files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
