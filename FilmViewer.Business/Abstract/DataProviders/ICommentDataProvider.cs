using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface ICommentDataProvider
    {
        List<CommentDto> GetCommentsForMovie(int movieId, int commentsCount);
    }
}
