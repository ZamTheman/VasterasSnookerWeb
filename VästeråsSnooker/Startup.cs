using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VästeråsSnooker.Startup))]
namespace VästeråsSnooker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
