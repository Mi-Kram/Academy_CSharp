using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class TitlesViewModel
	{
		public List<Title> Titles { get; set; } = new List<Title>();
		public List<Publisher> Publishers { get; set; } = new List<Publisher>();
	}
}
