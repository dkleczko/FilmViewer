using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Enums;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public AddResult AddCategory(CategoryDto categoryDto)
        {
            var isCategoryWithTheSameName = _uow.CategoryRepository.IsCategoryWithTheSameName(categoryDto.CategoryName);
            if (!isCategoryWithTheSameName)
            {
                _uow.CategoryRepository.Add(new Category()
                {
                    Name = categoryDto.CategoryName
                });
                var comp =  _uow.Complete();
                return comp > 0 ? AddResult.Added : AddResult.UnknownError;
            }
            return AddResult.DuplicateElement;
        }

        public void DeleteCategory(int categoryId)
        {
            var categoryEntity = _uow.CategoryRepository.Get(categoryId);
            _uow.CategoryRepository.Remove(categoryEntity);

            _uow.Complete();
        }

        public AddResult EditCategory(CategoryDto categoryDto)
        {
            var isCategoryWithTheSameName = _uow.CategoryRepository.IsCategoryWithTheSameName(categoryDto.CategoryName);
            if (!isCategoryWithTheSameName)
            {
                var categoryEntity = _uow.CategoryRepository.Get(categoryDto.CategoryId);

                categoryEntity.Name = categoryDto.CategoryName;

                var comp = _uow.Complete();

                return comp > 0 ? AddResult.Added : AddResult.UnknownError;
            }
            return AddResult.DuplicateElement;
        }
    }
}
