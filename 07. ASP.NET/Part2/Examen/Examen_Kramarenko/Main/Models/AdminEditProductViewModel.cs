using Main.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class AdminEditProductViewModel
	{
		public Product Product { get; set; } = null;
		public List<Category> Categories { get; set; } = new List<Category>();
	}
}
