using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XtraCoverBBGA.Startup))]
namespace XtraCoverBBGA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
