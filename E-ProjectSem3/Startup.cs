using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_ProjectSem3.Startup))]
namespace E_ProjectSem3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
