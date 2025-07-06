using System;
using CoreMVC_Identity.Areas.Identity.Data;
using CoreMVC_Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CoreMVC_Identity.Areas.Identity.IdentityHostingStartup))]
namespace CoreMVC_Identity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            // добавление контекста БД в проект
            /*builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ApplicationDBContextConnection")));

                // добавление Identity в проект
                //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                //    .AddEntityFrameworkStores<ApplicationDBContext>();
            });*/
        }
    }
}