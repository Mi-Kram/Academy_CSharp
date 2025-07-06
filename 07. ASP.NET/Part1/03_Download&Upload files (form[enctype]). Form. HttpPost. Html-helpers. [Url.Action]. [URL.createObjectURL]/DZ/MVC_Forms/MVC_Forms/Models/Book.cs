using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Forms.Models
{
    public class Book
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public String Genre { get; set; }
        public DateTime PubDate { get; set; }
        public int Price { get; set; }
        public Boolean hasAudioBook { get; set; }
    }
}