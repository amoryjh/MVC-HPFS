using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HPSMVC.Startup))]
namespace HPSMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
