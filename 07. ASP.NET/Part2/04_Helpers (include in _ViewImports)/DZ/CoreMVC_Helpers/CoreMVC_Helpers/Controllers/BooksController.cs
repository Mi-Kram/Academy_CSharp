using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreMVC_Helpers.Models;
using CoreMVC_Helpers.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC_Helpers.Controllers
{
    
    public class BooksController : Controller
    {
        public static Books books = new Books();

        // GET: Books
        public ActionResult Index(int pageNumber = 1)
        {
            // количество объектов на странице
            int pageSize = 4;

            // создать список книг для одной страницы
            IEnumerable<Book> pageBooksList = books.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // заполнить контейнер с информацией о странице
            PageInfo pageInfo = new PageInfo { PageNumber = pageNumber, PageSize = pageSize, TotalBooks = books.Count };

            // создать модель представления
            BooksIndexViewModel booksViewModel = new BooksIndexViewModel { PageInfo = pageInfo, Books = pageBooksList };
            
            return View(booksViewModel);
        }

        public ActionResult Helpers()
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

            // если книги с переданным id не существует - перейти к списку книг
            if(book == null)
                return RedirectToAction("Index", "Books");

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

            // передача параметров на форму при помощи словаря
            //ViewData["Books"] = books;

            // передача параметров на форму при помощи свойств
            //ViewBag.Books = books;

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
        public ActionResult Save(Book book)
        {
            // создаётся новая книга
            if (book.Id == 0)
            {
                book.Id = books.Id++;
                if(book.Title==null || book.Author==null || book.Genre==null)
                    return RedirectToAction("Index", "Books");
                books.Add(book);
            }
            else
            {
                var bookInDB = books.Single(c => c.Id == book.Id);

                if (book == null)
                    return RedirectToAction("Index", "Books");

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
    }
}