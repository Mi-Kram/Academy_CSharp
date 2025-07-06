using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_RazorPagesRouting.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Название книги")]
        [Required(ErrorMessage = "Please enter books's title.")]
        [StringLength(30, ErrorMessage = "Название книги не должно превышать 30 символов")]
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
        [Required(ErrorMessage = "Please enter books's pubdate.")]
        public DateTime PubDate { get; set; }

        [Required(ErrorMessage = "Please enter books's price.")]
        [Range(1, 1000)]
        public int Price { get; set; }

        public Boolean hasAudioBook { get; set; }
    }
}
