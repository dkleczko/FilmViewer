using FilmViewer.Business.Dto.Extended.Actor;
using PagedList;

namespace FilmViewer.Models.User
{
    public class VotedActorsViewModel
    {
        public IPagedList<VotedActorsByUserDto> VotedActors { get; set; }
    }
}