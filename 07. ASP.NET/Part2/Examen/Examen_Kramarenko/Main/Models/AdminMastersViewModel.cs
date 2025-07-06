using Main.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class AdminMastersViewModel
	{
		public List<ApplicationUser> Masters { get; set; } = new List<ApplicationUser>();
	}
}
