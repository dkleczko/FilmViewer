using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface ICategoryDataProvider
    {
        int GetAllCatagoriesCount();
        List<CategoryDto> GetCategoriesBySearchString(string searchString);
        CategoryDto GetCategoryById(int categoryId);
        List<CategoryDto> GetAllCategories();
    }
}
