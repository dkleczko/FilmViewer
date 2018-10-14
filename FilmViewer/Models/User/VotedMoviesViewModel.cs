using FilmViewer.Business.Dto.Extended.Movie;
using PagedList;

namespace FilmViewer.Models.User
{
    public class VotedMoviesViewModel
    {
        public IPagedList<VotedMoviesByUserDto> VotedMovies { get; set; }
    }
}