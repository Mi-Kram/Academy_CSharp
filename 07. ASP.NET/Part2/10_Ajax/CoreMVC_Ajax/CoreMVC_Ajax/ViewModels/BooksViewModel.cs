using CoreMVC_Ajax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreMVC_Ajax.ViewModels
{
    public class BooksViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}