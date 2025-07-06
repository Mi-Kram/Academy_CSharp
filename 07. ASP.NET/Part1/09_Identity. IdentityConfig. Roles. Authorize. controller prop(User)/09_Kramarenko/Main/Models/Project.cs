using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class Project
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(50)]
		public string Caption { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }

		public virtual ICollection<ApplicationUser> Employees { get; set; }

	}
}