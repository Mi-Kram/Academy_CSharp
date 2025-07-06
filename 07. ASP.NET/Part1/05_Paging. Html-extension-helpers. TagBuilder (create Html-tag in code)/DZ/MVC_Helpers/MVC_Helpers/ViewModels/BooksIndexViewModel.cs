using MVC_Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Helpers.ViewModels
{
    public class PageInfo
    {
        // номер текущей страницы
        public int PageNumber { get; set; }

        // количество книг на странице
        public int PageSize { get; set; }

        // общее количество книг
        public int TotalBooks { get; set; }

        // всего страниц
        public int TotalPages  
        {
            get { return (int)Math.Round((double)TotalBooks / PageSize); }
        }
    }

    public class BooksIndexViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}