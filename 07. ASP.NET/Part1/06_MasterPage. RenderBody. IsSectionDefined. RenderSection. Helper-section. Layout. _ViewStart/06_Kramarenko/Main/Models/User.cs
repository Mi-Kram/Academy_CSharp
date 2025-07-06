using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class User
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public bool IsAdmin { get; set; }
		public List<int> CarIDs { get; set; }
	}
}