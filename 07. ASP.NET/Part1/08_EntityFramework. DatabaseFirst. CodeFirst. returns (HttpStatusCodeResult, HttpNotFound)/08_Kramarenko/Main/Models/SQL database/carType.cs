using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Models.SQL_database
{
	public class carType
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int typeID { get; set; }

		[MaxLength(50)]
		public string value { get; set; }
	}
}