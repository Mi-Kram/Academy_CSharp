using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Areas.Identity.Data
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Required]
		public int ProductID { get; set; }

		[Required]
		public string MasterID { get; set; }

		[Required]
		public int ClientID { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime StartDate{ get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? EndDate { get; set; }

		[Required]
		public bool IsDone { get; set; }

		[Required]
		public double Salary { get; set; }

		[ForeignKey("MasterID")]
		public virtual ApplicationUser Master { get; set; }

		[ForeignKey("ClientID")]
		public virtual ApplicationClient Client { get; set; }

		[ForeignKey("ProductID")]
		public virtual Product Product{ get; set; }
	}
}
