using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Main.Models.SQL_database
{
	public class car
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int carID { get; set; }

		[MaxLength(50)]
		public string brand { get; set; }

		[MaxLength(50)]
		public string model { get; set; }

		public double speed { get; set; }

		public double price { get; set; }

		public DateTime date { get; set; }

		public int typeID { get; set; }

		[ForeignKey("typeID")]
		public carType Type { get; set; }

		[MaxLength(200)]
		public string imgSrc { get; set; }

		public virtual ICollection<person> people { get; set; }
	}
}