using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class Comment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Required]
		[MaxLength(200)]
		public string Content { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public int PictureID { get; set; }

		[ForeignKey("PictureID")]
		public virtual Picture Picture { get; set; }
	}
}
