using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_FileUpload.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home  
        public ActionResult UploadFiles()
        {
            return View();
        }

        public FileResult FileDownload()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/")+@"admin.gif");
            string fileName = "admin.gif";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Image.Gif, fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFiles(HttpPostedFileBase[] files)
        {
            // проверка корректности валидации
            if (ModelState.IsValid)
            {   
                // перебрать коллекцию полученных файлов
                foreach (HttpPostedFileBase file in files)
                {
                    // проверка наличия файла
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Server.MapPath("~/UploadedFiles/") + InputFileName;

                        // сохранение файла
                        file.SaveAs(ServerSavePath);
                    }

                }
                // установка сообщения о загрузке файлов
                ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
            }
            return View();
        }
    }
}