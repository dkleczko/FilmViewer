using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Enums;

namespace FilmViewer.Business.Abstract.Services
{
    public interface ICategoryService
    {
        AddResult AddCategory(CategoryDto categoryDto);
        void DeleteCategory(int categoryId);
        AddResult EditCategory(CategoryDto categoryDto);
    }
}
