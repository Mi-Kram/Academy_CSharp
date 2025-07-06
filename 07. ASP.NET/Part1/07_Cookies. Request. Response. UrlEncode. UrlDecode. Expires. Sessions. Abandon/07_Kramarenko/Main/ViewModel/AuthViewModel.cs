using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.ViewModel
{

	public class AuthViewModel
	{
		public bool IsLoginForm { get; set; }

		[Required(ErrorMessage = "Invalid login!")]
		[MinLength(3, ErrorMessage = "Login is too short!")]
		[MaxLength(15, ErrorMessage = "Login is too long!")]
		public string Login { get; set; } = "";

		[Required(ErrorMessage = "Invalid password!")]
		[MinLength(5, ErrorMessage = "Password is too short!")]
		[MaxLength(15, ErrorMessage = "Password is too long!")]
		public string Password { get; set; } = "";

		[Required(ErrorMessage = "Repeat the password!")]
		[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords are not same!")]
		public string RepeatPassword { get; set; } = "";


		public AuthViewModel() { }
		public AuthViewModel(bool _isLogin) 
		{
			IsLoginForm = _isLogin;
		}
	}
}