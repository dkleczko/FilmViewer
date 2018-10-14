using System.Collections.Generic;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;

namespace FilmViewer.Business.DataProviders
{
    public class CommentDataProvider : ICommentDataProvider
    {
        private readonly IUnitOfWork _uow;
        public CommentDataProvider(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public List<CommentDto> GetCommentsForMovie(int movieId, int commentsCount)
        {
            var comments = _uow.CommentRepository.GetTopNCommentsByMovieId(movieId, commentsCount);

            return BusinessMapper.Mapper.Map<List<CommentDto>>(comments);
        }
    }
}
