using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Routing.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            //return Content("<h1>Hello world!</h1>");
            //return new HttpNotFoundResult();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { param = "test1"});

            return View();
        }
    }
}