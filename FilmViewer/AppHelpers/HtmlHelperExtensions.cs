using System.IO;
using System.Web;
using System.Web.Mvc;

namespace FilmViewer.AppHelpers
{
    public static class HtmlHelperExtensions
    {
        public static string ImageOrDefault(this HtmlHelper helper, string filename)
        {
            var imagePath = uploadsDirectory + filename + ".png";
            var imageSrc = File.Exists(HttpContext.Current.Server.MapPath(imagePath))
                               ? imagePath : defaultImage;

            return imageSrc;
        }

        private static string defaultImage = "http://placehold.it/380x600";
        private static string uploadsDirectory = "~/Images/uploads/";
    }
}