using System.Collections.Generic;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.DataProviders
{
    public class MainMovieDataProvider : IMainMovieDataProvider
    {
        private readonly IUnitOfWork _uow;
        public MainMovieDataProvider(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public List<MainMoviesDto> GetAllMainMovies()
        {
            var allMainMovies = _uow.MainMovieRepository.GetAllMainMovies();
            return BusinessMapper.Mapper.Map<IEnumerable<MainMovie>, List<MainMoviesDto>>(allMainMovies);
        }
    }
}
