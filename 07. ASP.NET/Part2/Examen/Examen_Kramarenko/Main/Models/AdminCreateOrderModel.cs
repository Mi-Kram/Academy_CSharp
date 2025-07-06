using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class AdminCreateOrderModel
	{
		public int? OrderID { get; set; }

		[Required]
		public int? ProductID { get; set; }

		[Required]
		public int? ClientID { get; set; }

		[Required]
		public string MasterID { get; set; }

		[Required]
		public double? Salary { get; set; }

		// create or edit
		public string FormType { get; set; }

		public string Product { get; set; } = "";
		public string ProductYear { get; set; } = "";
		public string ProductSerialNumber{ get; set; } = "";

		public string Client { get; set; } = "";
		public string ClientBirthday { get; set; } = "";

		public string Master { get; set; } = "";
		public string MasterPhoto { get; set; } = "";
	}
}
