using FilmViewer.Business.Dto.Domain;
using PagedList;

namespace FilmViewer.Models.Admin
{
    public class IndexViewModel
    {
        public IPagedList<ApplicationUserDetailsDto> Users { get; set; }
    }
}