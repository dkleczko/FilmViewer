using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetTopNCommentsByMovieId(int movieId, int commentsCount);
    }
}
