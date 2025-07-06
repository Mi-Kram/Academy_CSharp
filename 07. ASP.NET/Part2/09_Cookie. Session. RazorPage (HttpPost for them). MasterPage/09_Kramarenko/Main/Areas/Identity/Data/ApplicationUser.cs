using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Models;
using Microsoft.AspNetCore.Identity;

namespace Main.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
		public virtual List<Picture> Pictures { get; set; }
		public virtual List<Comment> Comments { get; set; }
	}
}
