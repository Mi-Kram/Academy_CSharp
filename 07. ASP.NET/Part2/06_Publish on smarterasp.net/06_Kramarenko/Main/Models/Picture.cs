using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class Picture
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Required]
		public DateTime CreationDate { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }

		[Required]
		[MaxLength(20)]
		public string Caption { get; set; }

		[Required]
		[MaxLength(300)]
		public string ImgSrc { get; set; }

		[Required]
		public int CategoryID { get; set; }

		[ForeignKey("CategoryID")]
		public virtual Category Category { get; set; }

		public virtual List<Comment> Comments { get; set; }

	}
}
