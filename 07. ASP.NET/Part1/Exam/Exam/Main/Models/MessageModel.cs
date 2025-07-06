using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class MessageModel
	{
		public int ID { get; set; }

		public string Content { get; set; }

		public string SendTime { get; set; }

		public string SenderName { get; set; }

		public string SenderImg { get; set; }
	}
}