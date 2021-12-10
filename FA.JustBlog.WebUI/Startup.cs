using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FA.JustBlog.WebUI.Startup))]
namespace FA.JustBlog.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
