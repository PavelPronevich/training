using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HandH.Startup))]
namespace HandH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
