using FilmViewer.Business.Dto.Domain;
using PagedList;

namespace FilmViewer.Models.Moderator
{
    public class MovieViewViewModel
    {
        public IPagedList<MovieDetailsDto> MovieList { get; set; }
    }
}