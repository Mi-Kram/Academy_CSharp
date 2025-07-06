using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Main.Models.SQL_database
{
	public class person
	{
		[Key]
		[Required]
		[MaxLength(20)]
		public string login { get; set; }

		[MaxLength(100)]
		public string password { get; set; }

		public bool isAdmin { get; set; }

		public virtual ICollection<car> cars { get; set; }
	}
}