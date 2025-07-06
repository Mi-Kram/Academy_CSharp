using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.ViewModel
{
	public class MainCarPageViewModel
	{
		public User User { get; set; }
		public bool IsAdminToggle { get; set; }

		public List<CarModel> Cars { get; set; }
	}
}