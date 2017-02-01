//-----------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Inzynierka
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/detailsview").Include(
                        "~/Scripts/calculateMidLatLng.js",
                        "~/Scripts/markerwithlabel.js",
                         "~/Scripts/DiagonalsButton.js",
                          "~/Scripts/LatLngButton.js",
                           "~/Scripts/LinieButton.js",
                            "~/Scripts/LoadPoints.js",
                             "~/Scripts/GenerateLines.js",
                              "~/Scripts/PrzekatneButton.js",
                               "~/Scripts/CalculateAngles.js",
                                "~/Scripts/CenterMap.js",
                                "~/Scripts/PoleIObwodButton.js",
                                "~/Scripts/InitializeDetailsViewMap.js"
                                ));
            bundles.Add(new ScriptBundle("~/bundles/editview").Include(
                                "~/Scripts/calculateMidLatLng.js",
                               "~/Scripts/ManageModalWindow.js",
                        "~/Scripts/markerwithlabel.js",
                              "~/Scripts/PrzekatneButton.js",
                               "~/Scripts/ManageNotes.js",
                               "~/Scripts/CalculateAngles.js",
                             "~/Scripts/GenerateLinesEdit.js",
                             "~/Scripts/AddAndLoadPoint.js",
                                "~/Scripts/DiagonalsButton.js",
                                "~/Scripts/LinieButton.js",
                                "~/Scripts/CenterMap.js",
                                "~/Scripts/PoleIObwodButton.js",
                                "~/Scripts/InitializeEditViewMap.js"
                                ));
            bundles.Add(new ScriptBundle("~/bundles/createview").Include(
                               "~/Scripts/calculateMidLatLng.js",
                        "~/Scripts/markerwithlabel.js",
                               "~/Scripts/ManageModalWindow.js",
                              "~/Scripts/PrzekatneButton.js",
                               "~/Scripts/ManageNotes.js",
                               "~/Scripts/CalculateAngles.js",
                                "~/Scripts/DiagonalsButton.js",
                                "~/Scripts/LinieButton.js",
                                "~/Scripts/AddPoint.js",
                                "~/Scripts/PoleIObwodButton.js",
                               "~/Scripts/InitializeCreateViewMap.js"
                               ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Bootstrap").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/collapse.js",
                        "~/Scripts/transition.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
               "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}