using System.Web;
using System.Web.Optimization;

namespace NTEC
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725 
        public static void RegisterBundles(BundleCollection bundles)
        {



            bundles.Add(new ScriptBundle("~/bundles/login").Include(
           "~/Scripts/app/login/app.ui.login.js"));



            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/lib/jQuery/jqueryui/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/lib/jQuery/jquery.unobtrusive*",
                        "~/Scripts/lib/jQuery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/common/utilities.js",               
                "~/Scripts/common/json2.js"));

            bundles.Add(new ScriptBundle("~/bundles/appcore").Include(
                        "~/Scripts/app/core/app.js",
                        "~/Scripts/app/core/app.ui.enums.js",
                        "~/Scripts/app/core/app.globals.js",
                        "~/Scripts/app/core/app.resources.js",
                        "~/Scripts/app/core/app.debugger.js",
                        "~/Scripts/app/core/app.ui.js",
                        "~/Scripts/app/core/app.ui.common.js"));

            bundles.Add(new ScriptBundle("~/bundles/OthersCommen").Include(
                        "~/Scripts/lib/jquery.cookie.js",
                        "~/Scripts/lib/jquery.pnotify.js",
                        "~/Scripts/lib/spinner/spin.js",
                        "~/Scripts/lib/spinner/jquery.spin.js",
                        "~/Scripts/lib/spinner/ui.spinner.js",
                        "~/Scripts/lib/spinner/ui.spinner.min.js,",
                        "~/Scripts/lib/classie.js",
                        "~/Scripts/lib/handlebars.js",
                        "~/Scripts/lib/knockout-3.2.0.js",
                       
                        
                        "~/Scripts/lib/FineUploader/fineuploader.js",
                        "~/Scripts/lib/timePicker/jquery-ui-timepicker-addon.js"));
            // 
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/all.css",
                "~/Scripts/lib/timePicker/jquery-ui-timepicker-addon.css",
            "~/Content/PineNotifications/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/kendo.all.min.js",
            "~/Scripts/kendo/kendo.aspnetmvc.min.js"));
            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common-bootstrap.min.css",
                "~/Content/kendo/kendo.bootstrap.min.css"));
        }
    }
}