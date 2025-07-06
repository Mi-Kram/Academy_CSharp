using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Identity3.Startup))]
namespace MVC_Identity3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
