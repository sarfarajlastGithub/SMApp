using System.Web;
using System.Web.Optimization;

namespace SMApp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //For full Layout
            bundles.Add(new ScriptBundle("~/bundles/fulljquery").Include(
                        "~/Scripts/jquery-{version}.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/metisMenu.min.js",
                        "~/Scripts/TemplateJS/sb-admin-2.min.js"));



            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //for template purpose
            bundles.Add(new StyleBundle("~/Content/fullcss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/metisMenu.min.css",
                      "~/Content/TemplateCss/sb-admin-2.css",
                      "~/Content/font-awesome.min.css"));
        }
    }
}
