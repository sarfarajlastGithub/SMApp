using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMApp.Web.Startup))]
namespace SMApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
