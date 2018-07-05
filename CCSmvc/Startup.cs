using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CCSmvc.Startup))]
namespace CCSmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
