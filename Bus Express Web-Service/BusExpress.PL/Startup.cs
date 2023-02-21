using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusExpress.PL.Startup))]
namespace BusExpress.PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
