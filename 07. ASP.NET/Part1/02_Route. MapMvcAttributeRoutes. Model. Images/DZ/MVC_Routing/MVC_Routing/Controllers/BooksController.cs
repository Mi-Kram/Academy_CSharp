using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Routing.Models;

namespace MVC_Routing.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index(int? id)
        {
            return View(new Books());
        }

        [Route("idbooks/{id}")]
        public ActionResult BookById(int? id)
        {
            if (!id.HasValue)
                id = 1;

            var book = new Books().SingleOrDefault(c => c.Id == id);

            return View(book);
        }

        // GET: Books/Details/1
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                id = 1;

            var book = new Books().SingleOrDefault(c => c.Id == id);

            if(book == null)
                return RedirectToAction("Index", "Books");

            return View(book);
        }

        [Route("books/released/{year:range(1900, 2100)}")]
        public ActionResult BooksByYear(int year)
        {
            var filteredBooks = from p in new Books()
                                where p.PubDate.Year == year
                                select p;
            return View(filteredBooks);
        }

        [Route("books/genre/{genre:regex(^(Poetry|Drama|Sci-fi|Roman)$)}")]
        public ActionResult BooksByGenre(String genre)
        {
            var filteredBooks = from p in new Books()
                                where p.Genre.Equals(genre)
                                select p;
            return View(filteredBooks);
        }
    }
}