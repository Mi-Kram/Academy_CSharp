using CoreMVC_Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreMVC_Helpers.ViewModels
{
    public class BookFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public Book Book { get; set; }
    }
}