using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;
using FilmViewer.Business.Enums;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface IMovieDataProvider
    {
        List<Top6RatesMoviesDto> GeTop6RatesMovies();
        List<RecommendedMovieForUserDto> GetRecommendedMoviesForUser(ApplicationUser user);
        List<SearchMoviesDto> SearchMovies(string searchString, int? categoryId, SearchMovieBy searchBy, ApplicationUser user);
        List<MovieDto> GetTop10MostCommentedMovies();
        MovieDetailsExtendedDto GetMovieById(int id, ApplicationUser currentUser = null);
        List<MovieDto> GetSimilarMoviesToMovieId(int movieId);
        List<MovieDto> GetTop100BestRatedMovies();
        int GetCountOfAllMovies();
        MovieDto GetMovieDtoById(int movieId);
        List<MovieDetailsDto> SearchMovie(string searchString, SortMovieBy sortBy, SortOrder sortOrder);
    }
}
