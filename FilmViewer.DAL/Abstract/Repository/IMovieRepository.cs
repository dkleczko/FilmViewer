using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<Movie> GetTop6RatesMovies();
        IEnumerable<Movie> Get10RandomMovies();
        IEnumerable<Movie> GetAllMoviesExceptIds(List<int> movieIds);
        IEnumerable<Movie> GetAllMoviesWithMetadata();
        IEnumerable<Movie> SearchMovieByTitle(string searchString);
        IEnumerable<Movie> SearchMovieByDirectorName(string searchString);
        IEnumerable<Movie> SearchMovieByActorName(string searchString);
        IEnumerable<Movie> SearchMovieByCategoryId(int categoryId);
        IEnumerable<Movie> GetTop10MostCommentedMovies();
        Movie GetMovieById(int movieId);
        IEnumerable<Movie> GetTop100VotedMovies();
        Movie GetMovieByIdWithPhotos(int movieId);
    }
}
