using MVC_Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Forms.ViewModels
{
    public class BookFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public Book Book { get; set; }
    }
}