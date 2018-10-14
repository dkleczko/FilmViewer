using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Domain.Enum;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;
using FilmViewer.DAL.Model.Enum;

namespace FilmViewer.Business.Services
{
    public class MainMovieService : IMainMovieService
    {
        private readonly IUnitOfWork _uow;
        public MainMovieService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void AddMainMovie(SliderTypeDto sliderType, string sliderTitle, string sliderContent, string photoPath, int movieId)
        {
            var movieEntity = _uow.MovieRepository.Get(movieId);
            var mainMovie = new MainMovie()
            {
                Content = sliderContent,
                MovieImagePath = photoPath,
                SliderType = BusinessMapper.Mapper.Map<SliderType>(sliderType),
                Title = sliderTitle,
                Movie = movieEntity
            };
            _uow.MainMovieRepository.Add(mainMovie);

            _uow.Complete();
        }

        public void DeleteMainMovie(int mainMovieId)
        {
            var mainMovieEntity = _uow.MainMovieRepository.Get(mainMovieId);

            if (mainMovieEntity != null)
            {
                _uow.MainMovieRepository.Remove(mainMovieEntity);

                _uow.Complete();
            }
        }
    }
}
