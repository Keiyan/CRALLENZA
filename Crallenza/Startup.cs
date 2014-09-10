using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Crallenza.Startup))]
namespace Crallenza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
