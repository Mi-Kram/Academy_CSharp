using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Main.Pages.Settings
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public List<SiteThema> SiteThemas { get; set; } = new List<SiteThema>();
        public int CurrentThemaID { get; set; } = 1;

		public IndexModel(ApplicationDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        
        public async Task OnGetAsync()
        {
            SiteThemas = await _context.SiteThemas.ToListAsync();
            int? ct_id = HttpContext.Session.GetInt32("CurrentThemaID");
            if (ct_id.HasValue) CurrentThemaID = ct_id.Value;
			else if(SiteThemas.Count > 0)
			{
                CurrentThemaID = SiteThemas[0].ID;
                HttpContext.Session.SetInt32("CurrentThemaID", CurrentThemaID);
                HttpContext.Session.SetString("CurrentThema", SiteThemas[0].Background);
            }
        }
        
        public async Task<JsonResult> OnPostThemaAsync(int? id)
        {
            if (!id.HasValue) return new JsonResult(new { isSuccess = false });

            SiteThema thema = await _context.SiteThemas.Where(x => x.ID == id).FirstOrDefaultAsync();
            if(thema == null) return new JsonResult(new { isSuccess = false });

            return new JsonResult(new
            {
                isSuccess = true,
                background = thema.Background
            });
        }
        
        public async Task<JsonResult> OnPostSaveThemaAsync(int? id)
        {
            if (!id.HasValue) return new JsonResult(false);

            SiteThema thema = await _context.SiteThemas.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (thema == null) return new JsonResult(false);

            HttpContext.Session.SetInt32("CurrentThemaID", id.Value);
            HttpContext.Session.SetString("CurrentThema", thema.Background);
            return new JsonResult(true);
        }
    }
}
