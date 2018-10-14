using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EDirectorRepository : ERepository<Director>, IDirectorRepository
    {
        public EDirectorRepository(FilmViewerDbContext context) : base(context)
        {
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }

        public IEnumerable<Director> GetDirectorsBySearchString(string searchString)
        {
            return FilmViewerDbContext
                .Directors
                .Include(p => p.Votes)
                .Where(p => string.IsNullOrEmpty(searchString)
                            || p.Name.ToUpper().Contains(searchString.ToUpper()));
        }

        public Director GetDirectorByIdWithPhotoPaths(int directorId)
        {
            return FilmViewerDbContext
                .Directors
                .Include(p => p.PhotoUrls)
                .FirstOrDefault(p => p.Id == directorId);
        }

        public Director GetDirectorByIdWithPhotosMoviesVoteActor(int id)
        {
            return FilmViewerDbContext.Directors
                .Include(p => p.ConnectedMovies)
                .Include(p => p.PhotoUrls)
                .Include(p => p.Votes)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Director> GetTop100VotedDirectors()
        {
            return FilmViewerDbContext.Directors
                .Where(p => p.VoteCount > 0)
                .OrderByDescending(p => p.VoteScores / p.VoteCount).Take(100);
        }
    }
}
