using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class AddEmployeeToProjectViewModel
	{
		public List<ApplicationUser> Employees { get; set; }
		public int ProjectID { get; set; }
	}
}