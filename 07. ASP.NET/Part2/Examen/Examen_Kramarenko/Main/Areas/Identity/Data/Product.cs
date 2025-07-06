using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Areas.Identity.Data
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Required]
		public int CategoryID { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(20)")]
		public string SerialNumber { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public int YearIssue { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(20)")]
		public string Brand { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(20)")]
		public string Model { get; set; }

		[ForeignKey("CategoryID")]
		public virtual Category Category { get; set; }
	}
}
