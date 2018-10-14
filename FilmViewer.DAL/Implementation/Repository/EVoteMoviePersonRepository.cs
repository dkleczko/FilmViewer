using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EVoteMoviePersonRepository : ERepository<VoteMoviePerson>, IVoteMoviePersonRepository
    {
        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }
        public EVoteMoviePersonRepository(FilmViewerDbContext context) : base(context)
        {

        }

        public IEnumerable<VoteMoviePerson> GetAllVotesOfMoviePerson(int moviePersonId)
        {
            return FilmViewerDbContext.VoteMoviePersons
                .Include(p => p.MoviePerson)
                .Where(p => p.MoviePerson != null && p.MoviePerson.Id == moviePersonId);
        }
    }
}
