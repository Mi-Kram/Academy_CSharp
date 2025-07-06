using CoreMVC_Identity.Areas.Identity.Data;
using CoreMVC_Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CoreMVC_Identity.ViewModels
{
    public class AdministrationFormViewModel
    {
        public List<IdentityRole> Roles { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}