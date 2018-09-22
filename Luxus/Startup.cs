using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Luxus.Startup))]
namespace Luxus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
