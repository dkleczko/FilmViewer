using FilmViewer.Business.Dto.Domain;
using PagedList;

namespace FilmViewer.Models.User
{
    public class FavouritesViewModel
    {
        public IPagedList<MovieDto> FavouritesMovies { get; set; }
    }
}