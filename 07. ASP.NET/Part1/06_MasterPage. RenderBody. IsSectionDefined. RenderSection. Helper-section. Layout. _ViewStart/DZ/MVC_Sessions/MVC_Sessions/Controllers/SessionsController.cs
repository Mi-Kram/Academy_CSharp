using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Sessions.Controllers
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
            if (Request.Form["firstName"] != null)
                Session["firstName"] = Request.Form["firstName"];

            if (Request.Form["lastName"] != null)
                Session["lastName"] = Request.Form["lastName"];

            if (Request.Form["age"] != null)
                Session["age"] = Request.Form["age"];

            ViewBag.Message = "Data have been saved into sessions.";

            return View();
        }

        public ActionResult ReadSession()
        {
            if (Session["firstName"] != null)
                ViewBag.FirstName = Session["firstName"];
            else ViewBag.FirstName = "";

            if (Session["lastName"] != null)
                ViewBag.LastName = Session["lastName"];
            else ViewBag.LastName = "";

            if (Session["age"] != null)
                ViewBag.Age = Session["age"];
            else ViewBag.Age = "";

            ViewBag.SessionId = Session.SessionID;

            ViewBag.Message = "Contents of session:";

            return View();
        }

        public ActionResult RemoveFromSession()
        {
            // удалить всё из сессии
            Session.Abandon();

            /*Session.Remove("firstName");
            Session.Remove("lastName");
            Session.Remove("age");*/

            ViewBag.Message = "Сессия закрыта";

            return View();
        }
    }
}