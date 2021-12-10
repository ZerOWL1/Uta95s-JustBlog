using System.Web.Optimization;

namespace FA.JustBlog.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bootstrap
            bundles.Add(new StyleBundle("~/wwwroot/libs/download/bootstrap/css")
                .Include( "~/wwwroot/libs/download/bootstrap/css/bootstrap.min.css"));
            bundles.Add(new Bundle("~/wwwroot/libs/download/bootstrap/js")
                .Include( "~/wwwroot/libs/download/bootstrap/js/bootstrap.bundle.min.js"));
        }
    }
}
