using System.Collections.Generic;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class ECategoryRepository : ERepository<Category>, ICategoryRepository
    {
        public ECategoryRepository(FilmViewerDbContext context) : base(context)
        {
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }

        public IEnumerable<Category> GetCategoriesByIds(List<int> categoryIds)
        {
            return FilmViewerDbContext.Categories
                .Where(p => categoryIds.Any() && categoryIds.Contains(p.Id));
        }

        public IEnumerable<Category> GetCategoriesBySearchString(string searchString)
        {
            return FilmViewerDbContext
                .Categories
                .Where(p => string.IsNullOrEmpty(searchString) || p.Name.Contains(searchString));
        }

        public bool IsCategoryWithTheSameName(string categoryName)
        {
            return FilmViewerDbContext
                .Categories
                .Any(p => p.Name == categoryName);
        }
    }
}
