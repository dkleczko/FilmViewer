using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IActorRepository : IRepository<Actor>
    {
        IEnumerable<Actor> GetActorsBySearchString(string searchString);
        Actor GetActorByIdWithPhotosMoviesVoteActor(int id);
        IEnumerable<Actor> GetTop100VotedActors();
        IEnumerable<Actor> GetActorsByListOfIds(List<int> actorIds);
    }
}
