using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HPS.ASP.Startup))]
namespace HPS.ASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
