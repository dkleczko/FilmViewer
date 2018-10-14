using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class ECommentRepository : ERepository<Comment>, ICommentRepository
    {
        public ECommentRepository(FilmViewerDbContext context) : base(context)
        {
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }

        public IEnumerable<Comment> GetTopNCommentsByMovieId(int movieId, int commentsCount)
        {
            return FilmViewerDbContext.Comments
                .Include(p => p.Movie)
                .Include(p => p.User)
                .Where(p => p.Movie != null && p.Movie.Id == movieId)
                .OrderByDescending(p => p.DateTime).Take(commentsCount);
        }
    }
}
