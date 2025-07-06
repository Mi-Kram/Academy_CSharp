using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_MasterPage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Servises()
        {
            ViewBag.Message = "Servises page...";

            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "Gallery comes here...";

            return View();
        }
    }
}