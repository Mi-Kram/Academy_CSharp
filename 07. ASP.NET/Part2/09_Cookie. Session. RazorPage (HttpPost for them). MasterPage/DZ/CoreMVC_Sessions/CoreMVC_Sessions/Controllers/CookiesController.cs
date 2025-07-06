using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CoreMVC_Sessions.Controllers
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
            if (Request.Form.ContainsKey("firstName"))
            {
                // WebUtility.UrlEncode - решение проблемы с кодировками
                HttpContext.Response.Cookies.Append("firstName", WebUtility.UrlEncode(Request.Form["firstName"]),
                    new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            }

            if (Request.Form.ContainsKey("lastName"))
            {
                HttpContext.Response.Cookies.Append("lastName", WebUtility.UrlEncode(Request.Form["lastName"]),
                    new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            }

            if (Request.Form.ContainsKey("age"))
            {
                HttpContext.Response.Cookies.Append("age", WebUtility.UrlEncode(Request.Form["age"]),
                    new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            }

            ViewBag.Message = "Data have been saved into cookies.";

            return View();
        }

        public ActionResult ReadCookies()
        {
            if (Request.Cookies["firstName"] != null)
                ViewBag.FirstName = WebUtility.UrlDecode(Request.Cookies["firstName"]);
            else ViewBag.FirstName = "";

            if (Request.Cookies["lastName"] != null)
                ViewBag.LastName = WebUtility.UrlDecode(Request.Cookies["lastName"]);
            else ViewBag.LastName = "";

            /*if (Request.Cookies["age"] != null)
                ViewBag.Age = Request.Cookies["age"];
            else ViewBag.Age = "";*/

            ViewBag.Message = "Contents of cookies:";

            return View();
        }

        public ActionResult RemoveFromCookies()
        {
            HttpContext.Response.Cookies.Delete("firstName");
            HttpContext.Response.Cookies.Delete("lastName");
            HttpContext.Response.Cookies.Delete("age");

            ViewBag.Message = "Печенек больше нет";

            return View();
        }
    }
}
