using System.Web;
using System.Web.Optimization;

namespace FilmViewer
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles/menu")
                .Include(
                    "~/Content/Menu/navigate.css",
                    "~/Content/Menu/idangerous.swiper.css",
                    "~/Content/Menu/magnific-popup.css",
                    "~/Content/Menu/jquery.selectbox.css",
                    "~/Content/Menu/settings.css",
                    "~/Content/Menu/style.css"));

            bundles.Add(new StyleBundle("~/bundles/styles/dropzone")
                .Include("~/Content/Dropzone/dropzone.css"));

            bundles.Add(new StyleBundle("~/bundles/styles/datepicker")
                .Include("~/Content/Datepicker/default.css"));

            bundles.Add(new StyleBundle("~/bundles/styles/panelCreate")
                .Include("~/Content/panelCreate.css"));

            bundles.Add(new StyleBundle("~/bundles/styles/loopj")
                .Include("~/Content/Loopj/token-input-facebook.css"));

            bundles.Add(new StyleBundle("~/bundles/styles/bootstrap")
                .Include(
                    "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundle/styles/pagedList")
                .Include("~/Content/PagedList.css"));



            bundles.Add(new ScriptBundle("~/bundles/scripts/jquery").Include(
                        "~/Scripts/Jquery/jquery-1.10.2.min.js",
                        "~/Scripts/Jquery/idangerous.swiper.min.js",
                        "~/Scripts/Jquery/jquery-migrate-1.2.1.min.js",
                        "~/Scripts/Jquery/jquery.magnific-popup.min.js",
                        "~/Scripts/Jquery/jquery.mobile.menu.js",
                        "~/Scripts/Jquery/jquery.raty.js",
                        "~/Scripts/Jquery/jquery.selectbox-0.2.min.js",
                        "~/Scripts/Jquery/jquery.unobtrusive-ajax.js",
                        "~/Scripts/Jquery/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/menu")
                .Include(
                    "~/Scripts/Menu/modernizr.custom.js",
                    "~/Scripts/Menu/form-element.js",
                    "~/Scripts/Menu/form.js",
                    "~/Scripts/Menu/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/base")
                .IncludeDirectory("~/Scripts/Base", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/bootstrap")
                .IncludeDirectory("~/Scripts/Bootstrap", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/googleMap")
                .Include(
                "~/Scripts/Maps/infobox.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/revolution")
                .Include("~/Scripts/Revolution/jquery.themepunch.plugins.min.js")
                .Include("~/Scripts/Revolution/jquery.themepunch.revolution.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/datepicker")
                .Include(
                    "~/Scripts/Datepicker/zebra_datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/dropzone")
                .Include(
                    "~/Scripts/Dropzone/dropzone.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/loopj")
                .Include(
                    "~/Scripts/loopj/jquery.tokeninput.js"));

        }
    }
}
