using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class ProjectFormViewModel
	{
		[Required]
		[MaxLength(50)]
		public string Caption { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }
	}
}