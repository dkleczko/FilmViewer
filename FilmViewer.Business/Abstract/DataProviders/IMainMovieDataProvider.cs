using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface IMainMovieDataProvider
    {
        List<MainMoviesDto> GetAllMainMovies();
    }
}
