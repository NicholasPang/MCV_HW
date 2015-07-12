using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Practice.Startup))]
namespace MVC_Practice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
