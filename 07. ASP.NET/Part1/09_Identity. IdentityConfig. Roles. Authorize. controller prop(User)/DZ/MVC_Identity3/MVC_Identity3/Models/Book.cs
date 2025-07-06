using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Identity3.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter books's title.")]
        [StringLength(30)]
        public String Title { get; set; }

        [Required(ErrorMessage = "Please enter books's author.")]
        //[Compare("Title")]
        //[RegularExpression("^\\d+$")]
        //[Phone]
        //[EmailAddress]
        //[Url]
        public String Author { get; set; }

        [Display(Name = "Book's genre")]
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Please choose books's genre.")]
        public byte GenreId { get; set; }

        [Required(ErrorMessage = "Please enter books's pubdate.")]
        public DateTime PubDate { get; set; }

        [Required(ErrorMessage = "Please enter books's price.")]
        [Range(1, 1000)]
        public int Price { get; set; }
        public Boolean hasAudioBook { get; set; }
    }
}