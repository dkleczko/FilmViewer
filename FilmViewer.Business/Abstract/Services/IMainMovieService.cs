using FilmViewer.Business.Dto.Domain.Enum;

namespace FilmViewer.Business.Abstract.Services
{
    public interface IMainMovieService
    {
        void AddMainMovie(SliderTypeDto sliderType, string sliderTitle, string sliderContent, string photoPath, int movieId);
        void DeleteMainMovie(int mainMovieId);
    }
}
