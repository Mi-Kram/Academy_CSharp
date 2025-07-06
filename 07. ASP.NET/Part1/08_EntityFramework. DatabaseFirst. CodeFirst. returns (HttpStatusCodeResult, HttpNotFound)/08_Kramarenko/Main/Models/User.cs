using Main.Models.SQL_database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class User
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public bool IsAdmin { get; set; }


		public User() { }
		public User(person user)
		{
			Login = user.login;
			Password = user.password;
			IsAdmin = user.isAdmin;
		}
	}
}