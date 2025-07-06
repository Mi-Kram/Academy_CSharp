using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Sessions.Controllers
{
    public class CookiesController : Controller
    {
        // GET: Cookies
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save()
        {
            if (Request.Form["firstName"] != null)
            {
                // Server.UrlEncode - решение проблемы с кодировками
                Response.Cookies["firstName"].Value = Server.UrlEncode(Request.Form["firstName"]);

                // срок годности куков
                Response.Cookies["firstName"].Expires = DateTime.Now.AddDays(1);
            }

            if (Request.Form["lastName"] != null)
            {
                Response.Cookies["lastName"].Value = Server.UrlEncode(Request.Form["lastName"]);
                Response.Cookies["lastName"].Expires = DateTime.Now.AddDays(1);
            }

            if (Request.Form["age"] != null)
            {
                Response.Cookies["age"].Value = Request.Form["age"];
                Response.Cookies["age"].Expires = DateTime.Now.AddDays(1);
            }

            ViewBag.Message = "Data have been saved into cookies.";

            return View();
        }

        public ActionResult ReadCookies()
        {
            if (Request.Cookies["firstName"] != null)
                ViewBag.FirstName = Server.UrlDecode(Request.Cookies["firstName"].Value);
            else ViewBag.FirstName = "";

            if (Request.Cookies["lastName"] != null)
                ViewBag.LastName = Server.UrlDecode(Request.Cookies["lastName"].Value);
            else ViewBag.LastName = "";

            if (Request.Cookies["age"] != null)
                ViewBag.Age = Request.Cookies["age"].Value;
            else ViewBag.Age = "";

            ViewBag.Message = "Contents of cookies:";

            return View();
        }

        public ActionResult RemoveFromCookies()
        {
            // куки удаляются при помощи их устаревания на браузере
            Response.Cookies["firstName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["lastName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["age"].Expires = DateTime.Now.AddDays(-1);

            ViewBag.Message = "Печенек больше нет";

            return View();
        }
    }
}