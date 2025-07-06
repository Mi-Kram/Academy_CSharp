using Main.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class MasterCreateOrderViewModel
	{
		public string MyID { get; set; } = "";
		public List<Category> Categories { get; set; } = new List<Category>();
	}
}
