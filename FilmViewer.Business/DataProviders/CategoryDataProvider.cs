using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;

namespace FilmViewer.Business.DataProviders
{
    public class CategoryDataProvider : ICategoryDataProvider
    {
        private readonly IUnitOfWork _uow;
        public CategoryDataProvider(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public int GetAllCatagoriesCount()
        {
            var count = _uow.CategoryRepository.GetAll().Count();

            return count;
        }

        public List<CategoryDto> GetCategoriesBySearchString(string searchString)
        {
            var categories =  _uow.CategoryRepository.GetCategoriesBySearchString(searchString);
            return BusinessMapper.Mapper.Map<List<CategoryDto>>(categories);
        }

        public CategoryDto GetCategoryById(int categoryId)
        {
            var categoryEntity = _uow.CategoryRepository.Get(categoryId);

            return BusinessMapper.Mapper.Map<CategoryDto>(categoryEntity);
        }

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _uow.CategoryRepository.GetAll();
            return BusinessMapper.Mapper.Map<List<CategoryDto>>(categories);
        }
    }
}
