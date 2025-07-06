using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC_RazorPagesRouting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreMVC_RazorPagesRouting.Pages
{
    public class DetailsModel : PageModel
    {
        private Books allBooks = new Books();

        public Book book { get; set; }

        public void OnGet(int? id)
        {
            if (!id.HasValue)
                id = 1;

            book = allBooks.SingleOrDefault(c => c.Id == id);
        }
    }
}
