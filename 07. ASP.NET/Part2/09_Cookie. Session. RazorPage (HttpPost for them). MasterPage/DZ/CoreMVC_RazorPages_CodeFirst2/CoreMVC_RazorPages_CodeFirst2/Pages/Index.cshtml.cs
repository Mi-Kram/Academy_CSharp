using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreMVC_RazorPages_CodeFirst2.Models;

namespace CoreMVC_RazorPages_CodeFirst2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CoreMVC_RazorPages_CodeFirst2.Models.ApplicationContext _context;

        public IndexModel(CoreMVC_RazorPages_CodeFirst2.Models.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; }

        public async Task OnGetAsync()
        {
            Book = await _context.Books
                .Include(b => b.Genre).ToListAsync();
        }
    }
}
