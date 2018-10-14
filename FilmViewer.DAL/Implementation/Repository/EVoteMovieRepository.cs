using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EVoteMovieRepository : ERepository<VoteMovie>, IVoteMovieRepository
    {
        public EVoteMovieRepository(FilmViewerDbContext context) : base(context)
        {
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }

        public IEnumerable<VoteMovie> GetAllVotesMakesInMovieByUser(string userId, int movieId)
        {
            return FilmViewerDbContext.VoteMovies
                .Include(p => p.ApplicationUser)
                .Include(p => p.Movie)
                .Where(p => p.ApplicationUser != null && p.ApplicationUser.Id ==userId && p.Movie != null && p.Movie.Id == movieId);
        }

        public IEnumerable<VoteMovie> GetVotesByMovieId(int movieId)
        {
            return FilmViewerDbContext.VoteMovies
                .Include(p => p.Movie)
                .Where(p => p.Movie != null && p.Movie.Id == movieId);
        }
    }
}
