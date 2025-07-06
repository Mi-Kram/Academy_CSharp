using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Ajax.Models;

namespace MVC_Ajax.ViewModels
{
    public class BooksViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}