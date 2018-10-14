using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IVoteMoviePersonRepository : IRepository<VoteMoviePerson>
    {
        IEnumerable<VoteMoviePerson> GetAllVotesOfMoviePerson(int moviePersonId);
    }
}
