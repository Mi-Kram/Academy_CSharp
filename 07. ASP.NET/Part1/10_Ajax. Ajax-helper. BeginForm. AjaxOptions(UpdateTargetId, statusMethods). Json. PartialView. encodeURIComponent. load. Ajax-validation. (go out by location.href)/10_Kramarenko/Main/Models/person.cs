using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class person
	{
		[Required]
		[MaxLength(20)]
		public string FirsName { get; set; }

		[Required]
		[MaxLength(20)]
		public string LastName { get; set; }

		[Required]
		[Range(1, 120)]
		public int Age { get; set; }
	}
}