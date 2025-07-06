using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Areas.Identity.Data;
using Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Main.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<Title> Titles { get; set; }
		public DbSet<Publisher> Publishers { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
