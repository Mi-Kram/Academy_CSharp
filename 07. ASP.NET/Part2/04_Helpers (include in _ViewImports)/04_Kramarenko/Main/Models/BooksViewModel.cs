using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class BooksViewModel
	{
		public int Table1Page { get; set; } = 1;
		public int Table2Page { get; set; } = 1;
		public List<Title> Books { get; set; }

		public string Table1SortColumn { get; set; } = "Title";
		public bool Table1SortAsc { get; set; } = true;

		public string Table2SortColumn { get; set; } = "Title";
		public bool Table2SortAsc { get; set; } = true;
	}
}
