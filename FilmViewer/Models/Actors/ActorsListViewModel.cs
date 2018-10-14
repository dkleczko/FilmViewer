using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.DAL.Model;
using PagedList;
namespace FilmViewer.Models.Actors
{
    public class ActorsListViewModel
    {
        public IPagedList<ActorCurrentUserVoteDto> Actors { get; set; }
    }
}