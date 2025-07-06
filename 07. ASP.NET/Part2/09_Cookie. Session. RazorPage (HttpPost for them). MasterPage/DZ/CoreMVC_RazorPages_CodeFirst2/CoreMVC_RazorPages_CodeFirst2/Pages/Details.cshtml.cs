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
    public class DetailsModel : PageModel
    {
        private readonly CoreMVC_RazorPages_CodeFirst2.Models.ApplicationContext _context;

        public DetailsModel(CoreMVC_RazorPages_CodeFirst2.Models.ApplicationContext context)
        {
            _context = context;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books
                .Include(b => b.Genre).FirstOrDefaultAsync(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
