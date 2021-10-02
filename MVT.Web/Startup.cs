using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVT.Web.Startup))]
namespace MVT.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
