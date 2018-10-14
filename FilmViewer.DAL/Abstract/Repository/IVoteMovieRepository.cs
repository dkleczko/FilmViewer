using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IVoteMovieRepository : IRepository<VoteMovie>
    {
        IEnumerable<VoteMovie> GetAllVotesMakesInMovieByUser(string userId, int movieId);
        IEnumerable<VoteMovie> GetVotesByMovieId(int movieId);
    }
}
