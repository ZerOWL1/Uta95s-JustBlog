using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FA.JustBlog.WebUI.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest( AuthorizationContext filterContext )
        {
            var user = HttpContext.Current.Session["USER"];
            var mod = HttpContext.Current.Session["MODERATOR"];
            var admin = HttpContext.Current.Session["ADMIN"];

            if (mod != null || admin != null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "index",
                    controller = "admin",
                    area = "admin"
                }));
            }
            else if (user != null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "index",
                    controller = "post",
                    area = ""
                }));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "accessDenied",
                    controller = "admin",
                    area = "admin"
                }));
            }
        }
    }
}