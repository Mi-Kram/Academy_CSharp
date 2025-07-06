using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_WebAPI.Models;

namespace MVC_WebAPI.Controllers
{
    public class AuController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Au
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            return View();
        }

        public ActionResult New()
        {
            var author = new author();
            author.contract = false;

            return View("NewAuthor", author);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
                id = "172-32-1176";

            var au = db.authors.SingleOrDefault(c => c.au_id == id);

            if (au == null)
                return RedirectToAction("Index", "Au");

            return View("EditAuthor", au);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}