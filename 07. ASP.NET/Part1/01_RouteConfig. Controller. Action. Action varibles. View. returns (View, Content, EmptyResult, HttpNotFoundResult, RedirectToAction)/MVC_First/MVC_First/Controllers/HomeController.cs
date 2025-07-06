using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_First.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string name, int? age)
        {
            if(name != null && age.HasValue)
                return Content($"<html><body><h1>Hello {name}!!!</h1> You are {age} years old.</body></html>");
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Test(int? id, int? id2)
        {
            ViewBag.Message = "My test page!!!";

            // возврат "сырого" HTML-кода
            return Content($"<html><body><h1>Hello world!!!</h1>id = {id}<br>id2 = {id2}</body></html>");

            // возврат пустой страницы
            //return new EmptyResult();

            // стандартное сообщение ASP.NET "страница не найдена"
            //return new HttpNotFoundResult();

            // перенаправление на другую страницу
            //return RedirectToAction("Index", "Home", new { age = 23, name = "Alex" });

            //return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}