using System.Web;
using System.Web.Optimization;

namespace ussmain
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*")); 

            bundles.Add(new ScriptBundle("~/bundles/waypoint").Include(
                         "~/Scripts/jquery.waypoints.js"));
            bundles.Add(new ScriptBundle("~/bundles/steller").Include(
                        "~/Scripts/jquery.stellar.js"));
            bundles.Add(new ScriptBundle("~/bundles/ebrickkilnmainscript").Include(
                        "~/Scripts/ebrickkilnmainscript.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/project").Include( 
                  "~/Scripts/project.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapselect").Include(
                   "~/Scripts/bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include(
                    "~/Scripts/jquery.dataTables.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatablebootstrap").Include(
                   "~/Scripts/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                 "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapcalender").Include(
                 "~/Scripts/bootstrap-datepicker.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrapcalender").Include("~/Content/bootstrap-datepicker3.css"));

            bundles.Add(new StyleBundle("~/Content/ebrickstylestyle").Include("~/Content/ebrickstylestyle.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/superfish").Include("~/Content/superfish.css"));
            bundles.Add(new StyleBundle("~/Content/animate").Include("~/Content/animate.css"));
            bundles.Add(new StyleBundle("~/Content/icomoon").Include("~/Content/icomoon.css"));
            bundles.Add(new StyleBundle("~/Content/jquerydatatable").Include("~/Content/jquery.dataTables.css"));
            bundles.Add(new StyleBundle("~/Content/datatablebootstrap").Include("~/Content/dataTables.bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/projectstyle").Include("~/Content/projectstyle.css"));

            bundles.Add(new StyleBundle("~/Content/font").Include("~/Content/font-awesome.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrapselect").Include("~/Content/bootstrap-select.css"));




            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        //"~/Content/themes/base/jquery.ui.resizable.css",
                        //"~/Content/themes/base/jquery.ui.selectable.css",
                        //"~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        //"~/Content/themes/base/jquery.ui.button.css",
                        //"~/Content/themes/base/jquery.ui.dialog.css",
                        //"~/Content/themes/base/jquery.ui.slider.css",
                        //"~/Content/themes/base/jquery.ui.tabs.css",
                        //"~/Content/themes/base/jquery.ui.datepicker.css",
                        //"~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}