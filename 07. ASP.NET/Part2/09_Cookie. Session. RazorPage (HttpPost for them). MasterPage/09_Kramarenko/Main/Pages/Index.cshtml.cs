using Main.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1_RazorTest.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDBContext _context;
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger, ApplicationDBContext context)
		{
			_logger = logger;
			_context = context;
		}

		public void OnGet()
		{
			if (!HttpContext.Session.GetInt32("CurrentThemaID").HasValue)
			{
				var lst = _context.SiteThemas.ToList();
				if(lst.Count > 0)
				{
					var first = lst[0];
					HttpContext.Session.SetInt32("CurrentThemaID", first.ID);
					HttpContext.Session.SetString("CurrentThema", first.Background);
				}
			}
		}
	}
}
