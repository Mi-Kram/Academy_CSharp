using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Areas.Identity.Data
{
	public class ApplicationClient
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string FirstName { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string LastName { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(100)")]
		public string Address { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string PassportID { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime? Birthday { get; set; }

		public virtual List<Order> Orders { get; set; }
	}
}
