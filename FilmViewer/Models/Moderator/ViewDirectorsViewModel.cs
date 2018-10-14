using FilmViewer.Business.Dto.Domain;
using PagedList;

namespace FilmViewer.Models.Moderator
{
    public class ViewDirectorsViewModel
    {
        public IPagedList<DirectorDetailsDto> Directors { get; set; }
    }
}