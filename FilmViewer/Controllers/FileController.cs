using System.Web.Mvc;

namespace FilmViewer.Controllers
{
    public class FileController : BaseController
    {
        public ActionResult Image(string serverPath)
        {
            return base.File(serverPath, "mage/jpeg");
        }
        
    }
}