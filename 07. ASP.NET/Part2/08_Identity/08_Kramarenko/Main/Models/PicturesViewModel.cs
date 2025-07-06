using Main.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class PicturesViewModel
	{
		public List<Picture> Pictures { get; set; } = new List<Picture>();
		public List<Category> Categories { get; set; } = new List<Category>();
		public int CategoryID { get; set; } = -1;
		public bool IsAdmin { get; set; } = false;
	}
}
