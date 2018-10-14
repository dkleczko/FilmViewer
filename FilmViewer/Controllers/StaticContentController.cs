using System.Web.Mvc;

namespace FilmViewer.Controllers
{
    public class StaticContentController : BaseController
    {
        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult SomethingWrong()
        {
            return View();
        }
    }
}