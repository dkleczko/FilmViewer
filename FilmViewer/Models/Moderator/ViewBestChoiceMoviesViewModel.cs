using FilmViewer.Business.Dto.Domain;
using PagedList;

namespace FilmViewer.Models.Moderator
{
    public class ViewBestChoiceMoviesViewModel
    {
        public IPagedList<MainMoviesDto> MainMovies { get; set; }

    }
}