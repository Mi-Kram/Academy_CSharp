using Main.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class MasterCompletedViewModel
	{
		public List<Order> Orders { get; set; } = new List<Order>();
		public List<Category> Categories { get; set; } = new List<Category>();
	}
}
