using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
	public class Books : Controller
	{
		public IActionResult Index(int? table1Page, int? table2Page, string table1SortColumn, bool? table1SortAsc, string table2SortColumn, bool? table2SortAsc, int s = 5)
		{
			if (!table1Page.HasValue) table1Page = 1;
			if (!table2Page.HasValue) table2Page = 1;

			if (table1SortColumn == null) table1SortColumn = "Title";
			if (!table1SortAsc.HasValue) table1SortAsc = true;

			if (table2SortColumn == null) table2SortColumn = "Title";
			if (table2SortAsc == null) table2SortAsc = true;


			List<Title> books = new List<Title>();
			using (pubsContext db = new pubsContext())
			{
				books = db.Titles
					.Include(x => x.Titleauthors)
					.ThenInclude(x => x.Au).ToList();
			}

			BooksViewModel viewModel = new BooksViewModel()
			{
				Table1Page = table1Page.Value,
				Table2Page = table2Page.Value,
				Books = books,
				Table1SortColumn = table1SortColumn,
				Table1SortAsc = table1SortAsc.Value,
				Table2SortColumn = table2SortColumn,
				Table2SortAsc = table2SortAsc.Value
			};

			return View(viewModel);
		}
	}
}
