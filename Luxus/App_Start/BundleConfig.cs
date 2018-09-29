using System.Web;
using System.Web.Optimization;

namespace Luxus
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
            .IncludeDirectory("~/Scripts", "*.js", true));




            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/font-face.css",
                      "~/Content/vendor/font-awesome-4.7/css/font-awesome.min.css",
                      "~/Content/vendor/font-awesome-5/css/fontawesome-all.min.css",
                      "~/Content/vendor/mdi-font/css/material-design-iconic-font.min.css",
                      "~/Content/vendor/bootstrap-4.1/bootstrap.min.css",
                      "~/Content/vendor/animsition/animsition.min.css",
                      "~/Content/vendor/bootstrap-progressbar/bootstrap-progressbar-3.3.4.min.css",
                      "~/Content/vendor/wow/animate.css",
                      "~/Content/vendor/css-hamburgers/hamburgers.min.css",
                      "~/Content/vendor/slick/slick.css",
                      "~/Content/vendor/select2/select2.min.css",
                      "~/Content/vendor/perfect-scrollbar/perfect-scrollbar.css",
                      "~/Content/css/theme.css"
                      ));



            bundles.Add(new ScriptBundle("~/bundles/js")
            .Include("~/Content/vendor/bootstrap-4.1/popper.min.js",
            "~/Content/vendor/bootstrap-4.1/bootstrap.min.js",
            "~/Content/vendor/slick/slick.min.js",
            "~/Content/vendor/wow/wow.min.js",
            "~/Content/vendor/animsition/animsition.min.js",
            "~/Content/vendor/bootstrap-progressbar/bootstrap-progressbar.min.js",
            "~/Content/vendor/counter-up/jquery.waypoints.min.js",
            "~/Content/vendor/counter-up/jquery.counterup.min.js",
            "~/Content/vendor/circle-progress/circle-progress.min.js",
            "~/Content/vendor/perfect-scrollbar/perfect-scrollbar.js",
            "~/Content/vendor/chartjs/Chart.bundle.min.js",
            "~/Content/vendor/select2/select2.min.js",
            "~/Content/js/main.js"
            ));


            
        }
    }
}


