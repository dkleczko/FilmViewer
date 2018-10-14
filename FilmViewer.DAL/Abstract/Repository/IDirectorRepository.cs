using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IDirectorRepository : IRepository<Director>
    {
        IEnumerable<Director> GetDirectorsBySearchString(string searchString);
        Director GetDirectorByIdWithPhotoPaths(int directorId);
        Director GetDirectorByIdWithPhotosMoviesVoteActor(int id);
        IEnumerable<Director> GetTop100VotedDirectors();
    }
}
