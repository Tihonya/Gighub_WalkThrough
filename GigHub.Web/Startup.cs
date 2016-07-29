using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigHub.Web.Startup))]
namespace GigHub.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
