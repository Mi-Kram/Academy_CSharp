using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreMVC_Sessions.Controllers
{
    public class SessionsController : Controller
    {
        // GET: Sessions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save()
        {
            if (Request.Form.ContainsKey("firstName"))
                HttpContext.Session.SetString("firstName", Request.Form["firstName"]);

            if (Request.Form.ContainsKey("lastName"))
                HttpContext.Session.SetString("lastName", Request.Form["lastName"]);

            if (Request.Form.ContainsKey("age"))
                HttpContext.Session.SetString("age", Request.Form["age"]);

            ViewBag.Message = "Data have been saved into sessions.";

            return View();
        }

        public ActionResult ReadSession()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("firstName");
            if(ViewBag.FirstName == null)
                ViewBag.FirstName = "";

            ViewBag.LastName = HttpContext.Session.GetString("lastName");
            if (ViewBag.LastName == null)
                ViewBag.LastName = "";

            /*ViewBag.Age = HttpContext.Session.GetString("age");
            if (ViewBag.Age == null)
                ViewBag.Age = "";*/

            ViewBag.SessionId = HttpContext.Session.Id;

            ViewBag.Message = "Contents of session:";

            return View();
        }

        public ActionResult RemoveFromSession()
        {
            // удалить всё из сессии
            HttpContext.Session.Clear();

            /*HttpContext.Session.Remove("firstName");
            HttpContext.Session.Remove("lastName");
            HttpContext.Session.Remove("age");*/

            ViewBag.Message = "Сессия закрыта";

            return View();
        }
    }
}
