using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IMainMovieRepository : IRepository<MainMovie>
    {
        IEnumerable<MainMovie> GetAllMainMovies();
    }
}
