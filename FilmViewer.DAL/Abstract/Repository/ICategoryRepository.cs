using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesByIds(List<int> categoryIds);
        IEnumerable<Category> GetCategoriesBySearchString(string searchString);
        bool IsCategoryWithTheSameName(string categoryName);
    }
}
