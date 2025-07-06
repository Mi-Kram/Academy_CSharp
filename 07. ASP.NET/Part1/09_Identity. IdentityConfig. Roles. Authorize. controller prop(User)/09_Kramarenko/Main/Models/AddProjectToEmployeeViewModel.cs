using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class AddProjectToEmployeeViewModel
	{
		public List<Project> Projects { get; set; }
		public string UserLogin { get; set; }
	}
}