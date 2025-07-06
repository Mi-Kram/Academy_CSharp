using CoreMVC_Ajax.Models;
using CoreMVC_Ajax.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreMVC_Ajax.Controllers
{
    
    public class BooksController : Controller
    {
        public static Books books = new Books();

        // 1 способ, начальная страница
        public ActionResult Index()
        {
            var viewModel = new BooksViewModel
            {
                Books = books,
                Genres = new List<Genre>
                {
                    new Genre {Id = 1, Name = "Poetry" },
                    new Genre {Id = 2, Name = "Roman" },
                    new Genre {Id = 3, Name = "Drama" },
                    new Genre {Id = 4, Name = "Sci-fi" },
                    new Genre {Id = 5, Name = "Novel" },
                    new Genre {Id = 6, Name = "Comedy" },
                    new Genre {Id = 7, Name = "Detective" }
                }
            };

            return View(viewModel);
        }

        // 2 способ, начальная страница
        public ActionResult AjaxFormJson()
        {
            var viewModel = new BooksViewModel
            {
                Books = books,
                Genres = new List<Genre>
                {
                    new Genre {Id = 1, Name = "Poetry" },
                    new Genre {Id = 2, Name = "Roman" },
                    new Genre {Id = 3, Name = "Drama" },
                    new Genre {Id = 4, Name = "Sci-fi" },
                    new Genre {Id = 5, Name = "Novel" },
                    new Genre {Id = 6, Name = "Comedy" },
                    new Genre {Id = 7, Name = "Detective" }
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AjaxFormJson(string genre)
        {
            IEnumerable<Book> filteredBooks = null;

            if (genre != null)
                filteredBooks = from p in books
                                where p.Genre.Equals(genre)
                                select p;
            else
                filteredBooks = from p in books
                                select p;

            // преобразовать результирующую коллекцию в формат Json и возвратить
            var result = Json(filteredBooks, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });

            return result;
        }

        // 3, 4 способ, начальная страница
        public ActionResult JQueryAjax()
        {
            var viewModel = new BooksViewModel
            {
                Books = books,
                Genres = new List<Genre>
                {
                    new Genre {Id = 1, Name = "Poetry" },
                    new Genre {Id = 2, Name = "Roman" },
                    new Genre {Id = 3, Name = "Drama" },
                    new Genre {Id = 4, Name = "Sci-fi" },
                    new Genre {Id = 5, Name = "Novel" },
                    new Genre {Id = 6, Name = "Comedy" },
                    new Genre {Id = 7, Name = "Detective" }
                }
            };

            return View("JQueryAjaxForm", viewModel);
        }

        [HttpGet]
        [HttpPost]
        // метод возвращает PartialView, который содержит таблицу на Razor для возврата в исходную форму
        public ActionResult BooksList(string genre)
        {
            IEnumerable<Book> filteredBooks = null;

            if (genre != null)
                filteredBooks = from p in books
                                where p.Genre.Equals(genre)
                                select p;
            else
                filteredBooks = from p in books
                                select p;

            return PartialView(filteredBooks);
        }

        // метод вызывается из Ajax.Html и возвращает JSON, который содержит коллекцию результатов
        //[HttpPost]
        public JsonResult BooksListJson([FromBody] object body)
        {
            var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(body.ToString());
            string genre = dict["genre"];

            IEnumerable<Book> filteredBooks = null;

            if (genre != null)
                filteredBooks = from p in books
                                where p.Genre.Equals(genre)
                                select p;
            else
                filteredBooks = from p in books
                                select p;

            // преобразовать результирующую коллекцию в формат Json и возвратить

            var result = Json(filteredBooks, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });

            return result;
        }

        [HttpPost]
        // валидация AntiForgery на стороне сервера
        [ValidateAntiForgeryToken]
        public ActionResult Save(Book book)
        {
            // если форма не валидна
            if (!ModelState.IsValid)
            {
                var viewModel = new BookFormViewModel
                {
                    Book = book,
                    Genres = new List<Genre>
                    {
                        new Genre {Id = 1, Name = "Poetry" },
                        new Genre {Id = 2, Name = "Roman" },
                        new Genre {Id = 3, Name = "Drama" },
                        new Genre {Id = 4, Name = "Sci-fi" },
                        new Genre {Id = 5, Name = "Novel" },
                        new Genre {Id = 6, Name = "Comedy" },
                        new Genre {Id = 7, Name = "Detective" }
                    }
                };

                return PartialView("ValidationForm", viewModel);
            }

            if (book.Id == 0)
            {
                book.Id = books.Id++;
                if (book.Title == null || book.Author == null || book.Genre == null)
                    return RedirectToAction("Index", "Books");
                books.Add(book);

                return PartialView("Save");
            }
            else
            {
                var bookInDB = books.Single(c => c.Id == book.Id);

                bookInDB.Title = book.Title;
                bookInDB.Author = book.Author;
                if (book.Genre != null)
                    bookInDB.Genre = book.Genre;
                bookInDB.PubDate = book.PubDate;
                bookInDB.Price = book.Price;
            }

            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        // валидация JQuery Ajax
        //[ValidateAntiForgeryToken]
        public ActionResult Save2([FromBody] Book book)
        {
            //Book book = System.Text.Json.JsonSerializer.Deserialize<Book>(body.ToString());

            // если форма не валидна
            if (!ModelState.IsValid)
            {
                var errorModel =
                        from x in ModelState.Keys
                        where ModelState[x].Errors.Count > 0
                        select new
                        {
                            key = x,
                            errors = ModelState[x].Errors.Select(y => y.ErrorMessage).ToArray()
                        };

                return Json(new { success = false, errors = errorModel });
            }

            if (book.Id == 0)
            {
                book.Id = books.Id++;
                if (book.Title == null || book.Author == null || book.Genre == null)
                    return RedirectToAction("Index", "Books");
                books.Add(book);

                return Json(new { success = true, message = "Validation successfull!!!" });
            }
            else
            {
                var bookInDB = books.Single(c => c.Id == book.Id);

                bookInDB.Title = book.Title;
                bookInDB.Author = book.Author;
                if (book.Genre != null)
                    bookInDB.Genre = book.Genre;
                bookInDB.PubDate = book.PubDate;
                bookInDB.Price = book.Price;
            }

            return RedirectToAction("Index", "Books");
        }

        public ActionResult AjaxValidationForm()
        {
            var book = new Book();

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = new List<Genre>
                {
                    new Genre {Id = 1, Name = "Poetry" },
                    new Genre {Id = 2, Name = "Roman" },
                    new Genre {Id = 3, Name = "Drama" },
                    new Genre {Id = 4, Name = "Sci-fi" },
                    new Genre {Id = 5, Name = "Novel" },
                    new Genre {Id = 6, Name = "Comedy" },
                    new Genre {Id = 7, Name = "Detective" }
                }
            };

            return View(viewModel);
        }

        public ActionResult JQueryValidationForm()
        {
            var viewModel = new BookFormViewModel
            {
                Book = new Book(),
                Genres = new List<Genre>
                {
                    new Genre {Id = 1, Name = "Poetry" },
                    new Genre {Id = 2, Name = "Roman" },
                    new Genre {Id = 3, Name = "Drama" },
                    new Genre {Id = 4, Name = "Sci-fi" },
                    new Genre {Id = 5, Name = "Novel" },
                    new Genre {Id = 6, Name = "Comedy" },
                    new Genre {Id = 7, Name = "Detective" }
                }
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidateTitle([Bind(Prefix = "Book.Title")] string title)
        {
            try
            {
                if (title.Contains("a"))
                    return Json(true);
            }
            catch
            {
                return Json(false);
            }

            return Json(false);
        }
    }

    // кастомная серверная проверка на правильность даты
    public sealed class DateCheckAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                DateTime date = (DateTime)value;
                return (date <= DateTime.Now);
            }
            catch
            {
                return false;
            }
        }
    }
}