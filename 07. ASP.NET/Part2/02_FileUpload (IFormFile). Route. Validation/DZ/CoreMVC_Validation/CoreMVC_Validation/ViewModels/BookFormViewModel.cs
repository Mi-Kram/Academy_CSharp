using CoreMVC_Validation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreMVC_Validation.ViewModels
{
    public class BookFormViewModel
    {
        [Display(Name = "Жанр книги")]
        public IEnumerable<Genre> Genres { get; set; }

        public Book Book { get; set; }
    }
}