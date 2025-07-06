using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Validation;
using MVC_Validation.Controllers;

// validation help
// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.0

namespace MVC_Validation.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Название книги")]
        [Required(ErrorMessage = "Please enter books's title.")]
        [StringLength(30, ErrorMessage = "Название книги не должно превышать 30 символов")]
        [Remote("ValidateTitle", "Books", HttpMethod = "POST", ErrorMessage = "Title should contain letter 'a'")]
        public String Title { get; set; }

        [Display(Name = "Автор книги")]
        [Required(ErrorMessage = "Please enter books's author.")]
        //[Compare("Title")]
        //[RegularExpression("^\\d+$")]
        //[Phone]
        //[EmailAddress]
        //[Url]
        public String Author { get; set; }

        [Required(ErrorMessage = "Please enter books's genre.")]
        public String Genre { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата публикации")]
        [DateCheck(ErrorMessage = "Дата должна быть не позже сегодняшнего дня!")]
        [Required(ErrorMessage = "Please enter books's pubdate.")]
        public DateTime PubDate { get; set; }

        [Required(ErrorMessage = "Please enter books's price.")]
        [Range(1, 1000)]
        public int Price { get; set; }

        public Boolean hasAudioBook { get; set; }
    }
}