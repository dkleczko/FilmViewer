using FilmViewer.Business.Dto.Domain;
using PagedList;

namespace FilmViewer.Models.Moderator
{
    public class ViewActorsViewModel
    {
        public IPagedList<ActorDetailsDto> Actors { get; set; }
    }
}