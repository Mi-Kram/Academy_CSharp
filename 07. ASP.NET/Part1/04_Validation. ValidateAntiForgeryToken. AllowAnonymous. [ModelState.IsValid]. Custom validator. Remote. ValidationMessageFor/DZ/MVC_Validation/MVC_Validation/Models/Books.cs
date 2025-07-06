using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Validation.Models
{
    public class Books: List<Book>
    {
        public int Id { get; set; }
        public Books()
        {
            Add(new Book { Id = 1, Title = "Fairy tales", Author = "Pushkin", Genre = "Poetry", Price = 123, PubDate = DateTime.Parse("12/12/1998") });
            Add(new Book { Id = 2, Title = "Ruslan & Ludmila", Author = "Pushkin", Genre = "Poetry", Price = 67, PubDate = DateTime.Parse("12/12/1989") });
            Add(new Book { Id = 3, Title = "Aelita", Author = "Tolstoy", Genre = "Sci-fi", Price = 167, PubDate = DateTime.Parse("12/12/1978") });
            Add(new Book { Id = 4, Title = "Преступление и наказание", Author = "Dostoevsky", Genre = "Drama", Price = 231, PubDate = DateTime.Parse("12/12/1968") });
            Add(new Book { Id = 5, Title = "Poems", Author = "Lermontov", Genre = "Poetry", Price = 321, PubDate = DateTime.Parse("12/12/1991") });
            Add(new Book { Id = 6, Title = "Туманность Андромеды", Author = "Efremov", Genre = "Sci-fi", Price = 233, PubDate = DateTime.Parse("12/12/1998") });
            Add(new Book { Id = 7, Title = "Sherlock Holmes", Author = "Konan-Doyle", Genre = "Roman", Price = 322, PubDate = DateTime.Parse("12/12/1987") });
            Id = this.Count + 1;
        }
    }
}