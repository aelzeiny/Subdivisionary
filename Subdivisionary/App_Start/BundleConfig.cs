using System.Web;
using System.Web.Optimization;

namespace Subdivisionary
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            /*bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/site-js/FormScripts/BasicProjectInfo.js",
                        "~/Scripts/site-js/FormScripts/CocAndLlaProjectInfo.js",
                        "~/Scripts/site-js/FormScripts/UnitHistoryForm.js",
                        "~/Scripts/site-js/site-fileinput.js",
                        "~/Scripts/site-js/site-icollection.js",
                        "~/Scripts/site-js/site-jsignature.js"));*/

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jSignature.min.js",
                      "~/Scripts/fileinput.js",
                      "~/Content/bootstrap-fileinput/themes/fa/theme.js",
                      "~/Scripts/toastr.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-cerulean.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-fileinput/css/fileinput.css",
                      "~/Content/BsmStyle.css"));
        }
    }
}
