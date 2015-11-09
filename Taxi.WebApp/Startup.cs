using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Taxi.WebApp.Startup))]
namespace Taxi.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
