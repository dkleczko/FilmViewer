using FilmViewer.Business.Dto.Domain;
using PagedList;

namespace FilmViewer.Models.Moderator
{
    public class CategoryViewViewModel
    {
        public IPagedList<CategoryDto> CategoryList { get; set; }
    }
}