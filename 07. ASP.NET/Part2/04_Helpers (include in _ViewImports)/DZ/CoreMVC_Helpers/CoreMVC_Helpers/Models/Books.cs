using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreMVC_Helpers.Models
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

            Add(new Book { Id = 8, Title = "Сказки", Author = "Некрасов", Genre = "Poetry", Price = 123, PubDate = DateTime.Parse("12/12/1998") });
            Add(new Book { Id = 9, Title = "Колобок", Author = "Народ", Genre = "Poetry", Price = 67, PubDate = DateTime.Parse("12/12/1989") });
            Add(new Book { Id = 10, Title = "Отцы и дети", Author = "Тургенев", Genre = "Roman", Price = 167, PubDate = DateTime.Parse("12/12/1978") });
            Add(new Book { Id = 11, Title = "Война и мир", Author = "Толстой", Genre = "Roman", Price = 231, PubDate = DateTime.Parse("12/12/1968") });
            Add(new Book { Id = 12, Title = "Капитанская дочка", Author = "Pushkin", Genre = "Roman", Price = 321, PubDate = DateTime.Parse("12/12/1991") });
            Add(new Book { Id = 13, Title = "Маленький принц", Author = "Сент-Экзюпери", Genre = "Roman", Price = 233, PubDate = DateTime.Parse("12/12/1998") });
            Add(new Book { Id = 14, Title = "Дон Хуан", Author = "Кастаннеда", Genre = "Roman", Price = 322, PubDate = DateTime.Parse("12/12/1987") });
            Id = this.Count + 1;
        }
    }
}