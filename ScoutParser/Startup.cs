using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebParser.Startup))]
namespace WebParser
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
