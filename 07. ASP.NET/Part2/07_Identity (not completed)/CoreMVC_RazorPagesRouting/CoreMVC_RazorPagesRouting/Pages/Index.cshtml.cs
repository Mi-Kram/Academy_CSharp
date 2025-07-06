using CoreMVC_RazorPagesRouting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_RazorPagesRouting.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private static Books allBooks = new Books();

        public string Name { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string name)
        {
            if (name != null)
                Name = name;

            Books = allBooks;
        }

        public void OnGetByYear(int year)
        {
            var filteredBooks = from p in allBooks
                                where p.PubDate.Year == year
                                select p;
            Books = filteredBooks;
        }

        public void OnGetByGenre(String genre)
        {
            var filteredBooks = from p in allBooks
                                where p.Genre.Equals(genre)
                                select p;
            Books = filteredBooks;
        }
    }
}
