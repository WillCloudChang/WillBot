using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WillBot.Startup))]
namespace WillBot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
