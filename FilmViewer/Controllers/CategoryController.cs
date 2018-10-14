using System.Web.Mvc;
using FilmViewer.Business.Abstract.DataProviders;

namespace FilmViewer.Controllers
{
    public class CategoryController : BaseController
    {
        private ICategoryDataProvider _categoryDataProvider;
        public CategoryController(ICategoryDataProvider categoryDataProvider)
        {
            _categoryDataProvider = categoryDataProvider;
        }

        [OutputCache(Duration = 240)]
        public PartialViewResult GetAllCategoriesToMenu()
        {
            var allCategories = _categoryDataProvider.GetAllCategories();

            return PartialView(allCategories);
        }
    }
}