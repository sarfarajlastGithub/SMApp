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
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/metisMenu.min.js",
                        "~/Scripts/TemplateJS/sb-admin-2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jQueryUiJs").Include(
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/ajaxUnobs").Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // for jQuery dataTable Js
            bundles.Add(new ScriptBundle("~/bundles/datatbljs").Include(
                "~/Scripts/DataTable/jquery.dataTables.min.js",
                "~/Scripts/DataTable/dataTables.bootstrap.js"));

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

            bundles.Add(new StyleBundle("~/Content/jQueryUiCss").Include(
                        "~/Content/themes/base/jquery-ui.min.css"));

            // for jQuery dataTable css
            bundles.Add(new StyleBundle("~/Content/datatblcss").Include(
                "~/Content/DataTable/jquery.dataTables.min.css"));
        }
    }
}
