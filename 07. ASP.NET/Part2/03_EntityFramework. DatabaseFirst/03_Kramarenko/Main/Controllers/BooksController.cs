using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Main.Data;
using Main.Models;

namespace Main.Controllers
{
    public class BooksController : Controller
    {
        private readonly pubsContext _context;

        public BooksController(pubsContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
			//List<string> types = await _context.Titles.Select(x => x.Type).Distinct().ToListAsync();

			//Dictionary<string, List<BookModel>> books = types.ToDictionary(key => key,
			//    value => _context.Titles.Where(x => x.Type == value).Select(x => new BookModel()
			//    {
			//        Title = x.Title1,
			//        Price = (double)(x.Price ?? 0),
			//        Author = x.Titleauthors.FirstOrDefault().Au.AuFname,
			//        Type = x.Type,
			//        PubDate = x.Pubdate
			//    }).ToList());

			Dictionary<string, List<BookModel>> books = _context.Titles
				.Include(x => x.Titleauthors)
				.ThenInclude(x => x.Au).ToList()
				.GroupBy(x => x.Type)
				.ToDictionary(x => x.Key,
					value => value.Select(x => new BookModel()
					{
						Title = x.Title1,
						Price = (double)(x.Price ?? 0),
						Author = string.Join(", ", x.Titleauthors.Select(x => x.Au.AuFname)) ?? "",
						Type = x.Type,
						PubDate = x.Pubdate
					}).ToList());

			return View(books);
        }
    }
}
