using Microsoft.Owin;
using Owin;
using PrisonApp;

[assembly: OwinStartup(typeof(Startup))]

namespace PrisonApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}