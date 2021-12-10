using System.Web.Mvc;
using System.Web.Routing;

namespace FA.JustBlog.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "post",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Account", id = UrlParameter.Optional },
                namespaces: new[] {"FA.JustBlog.WebUI.Controllers"}
            );
        }
    }
}
