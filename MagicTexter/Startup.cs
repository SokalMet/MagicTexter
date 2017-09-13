using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MagicTexter.Startup))]
namespace MagicTexter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
