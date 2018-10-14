using System.Collections.Generic;
using System.Data.Entity;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EMainMovieRepository : ERepository<MainMovie>, IMainMovieRepository
    {
        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }
        public EMainMovieRepository(FilmViewerDbContext context) : base(context)
        {

        }

        public IEnumerable<MainMovie> GetAllMainMovies()
        {
            return FilmViewerDbContext.MainMovie
                .Include(p => p.Movie);
        }
    }
}
