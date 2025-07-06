using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Validation.Models;
using MVC_Validation.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MVC_Validation.Controllers
{
    
    public class BooksController : Controller
    {
        public static Books books = new Books();

        // GET: Books
        public ActionResult Index(int? id)
        {
            return View(books);
        }

        [Route("books/{id:regex(^\\d+$)}")]
        public ActionResult BookById(int? id)
        {
            if (!id.HasValue)
                id = 1;

            var book = books.SingleOrDefault(c => c.Id == id);

            return View(book);
        }

        // GET: Books/Details/1
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                id = 1;

            var book = books.SingleOrDefault(c => c.Id == id);

            return View(book);
        }

        [Route("books/released/{year:range(1900, 2100)}")]
        public ActionResult BooksByYear(int year)
        {
            var filteredBooks = from p in books
                                where p.PubDate.Year == year
                                select p;
            return View(filteredBooks);
        }

        [Route("books/genre/{genre:regex(^(Poetry|Drama|Sci-fi|Roman)$)}")]
        public ActionResult BooksByGenre(String genre)
        {
            var filteredBooks = from p in books
                                where p.Genre.Equals(genre)
                                select p;
            return View(filteredBooks);
        }

        // GET: Books/Edit/1
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                id = 1;

            var book = books.SingleOrDefault(c => c.Id == id);

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

            return View("BookForm", viewModel);
        }

        public ActionResult New()
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

            return View("BookForm", viewModel);
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

                return View("BookForm", viewModel);
            }

            if (book.Id == 0)
            {
                book.Id = books.Id++;
                if(book.Title==null||book.Author==null||book.Genre==null)
                    return RedirectToAction("Index", "Books");
                books.Add(book);
            }
            else
            {
                if(book.Title == "atest")
                {
                    // программный вызов ошибки валидации
                    ModelState.AddModelError("Book.Title", "Test error message!!!");

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

                    return View("BookForm", viewModel);
                }

                var bookInDB = books.Single(c => c.Id == book.Id);

                bookInDB.Title = book.Title;
                bookInDB.Author = book.Author;
                if (book.Genre != null)
                    bookInDB.Genre = book.Genre;
                bookInDB.PubDate = book.PubDate;
                bookInDB.Price = book.Price;
                bookInDB.hasAudioBook = book.hasAudioBook;
            }

            return RedirectToAction("Index", "Books");
        }

        public ActionResult Delete(int? id)
        {
            var bookInDB = books.Single(c => c.Id == id);
            if(bookInDB!=null)
            {
                books.Remove(bookInDB);
            }

            return RedirectToAction("Index", "Books");
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