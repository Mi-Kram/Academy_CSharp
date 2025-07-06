using Main.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class AdminOrdersViewModel
	{
		public List<ApplicationUser> Masters { get; set; } = new List<ApplicationUser>();
		public List<Order> Orders { get; set; } = new List<Order>();
	}
}
