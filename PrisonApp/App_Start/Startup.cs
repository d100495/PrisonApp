using Microsoft.Owin;
using Owin;
using PrisonApplication;


[assembly: OwinStartup(typeof(Startup))]

namespace PrisonApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}