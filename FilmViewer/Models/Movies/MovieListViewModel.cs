using FilmViewer.Business.Dto.Extended.Movie;
using PagedList;
namespace FilmViewer.Models.Movies
{
    public class MovieListViewModel
    {
        public IPagedList<SearchMoviesDto> Movies { get; set; }
    }
}