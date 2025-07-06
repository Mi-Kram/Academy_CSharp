using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class UpdateMasterModel
	{
		public string Id { get; set; }
		[Required]
		public string UserName { get; set; }
        public IFormFile Photo { get; set; }
		public bool ChangePassword { get; set; } = false;
		public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
