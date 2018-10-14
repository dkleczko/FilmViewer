using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;
using FilmViewer.Business.Enums;
using FilmViewer.Business.Mappings;
using FilmViewer.Business.RecommendationsEngine;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.DataProviders
{
    public class MovieDataProvider : IMovieDataProvider
    {
        private readonly IUnitOfWork _uow;
        public MovieDataProvider(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Top6RatesMoviesDto> GeTop6RatesMovies()
        {
            var top6Movies = _uow.MovieRepository.GetTop6RatesMovies();
            return BusinessMapper.Mapper.Map<List<Top6RatesMoviesDto>>(top6Movies);
        }

        public List<RecommendedMovieForUserDto> GetRecommendedMoviesForUser(ApplicationUser user)
        {
            List<Movie> recommendedMovies;
            if (user != null)
            {
                Recommendation.Initialize(_uow);
                recommendedMovies = Recommendation.PrepareMovieForUser(user, ViewType.MainView);
            }
            else
            {
                recommendedMovies = _uow.MovieRepository.Get10RandomMovies().ToList();
            }

            return BusinessMapper.Mapper.Map<List<Movie>, List<RecommendedMovieForUserDto>>(recommendedMovies);
        }

        public List<SearchMoviesDto> SearchMovies(string searchString, int? categoryId, SearchMovieBy searchBy, ApplicationUser currentUser)
        {
            IEnumerable<Movie> moviesFromDb;
            var resultList = new List<SearchMoviesDto>();
            switch (searchBy)
            {
                case SearchMovieBy.Title:
                    moviesFromDb = _uow.MovieRepository.SearchMovieByTitle(searchString);
                    break;
                case SearchMovieBy.ActorName:
                    moviesFromDb = _uow.MovieRepository.SearchMovieByActorName(searchString);
                    break;
                case SearchMovieBy.Category:
                    moviesFromDb = _uow.MovieRepository.SearchMovieByCategoryId(categoryId ?? 0);
                    break;
                case SearchMovieBy.DirectorName:
                    moviesFromDb = _uow.MovieRepository.SearchMovieByDirectorName(searchString);
                    break;
                default:
                    moviesFromDb = _uow.MovieRepository.SearchMovieByTitle(null);
                    break;
            }
            foreach (var movieFromDb in moviesFromDb)
            {
                var movieDto = BusinessMapper.Mapper.Map<Movie, SearchMoviesDto>(movieFromDb);
                if (currentUser != null)
                {
                    var userVote = movieFromDb.VoteMovie.FirstOrDefault(p => p.ApplicationUser != null  && p.ApplicationUser.Id == currentUser.Id);
                    if (userVote != null)
                    {
                        movieDto.CurrentUserVote = userVote.VoteScore;
                        movieDto.HasUserVoteActor = true;
                    }
                    movieDto.IsUserFavourite = movieFromDb.Favourites.Any(p => p.Id == currentUser.Id);
                }
                resultList.Add(movieDto);
            }
            return resultList;
        }

        public List<MovieDto> GetTop10MostCommentedMovies()
        {
            var top10Movies = _uow.MovieRepository.GetTop10MostCommentedMovies();
            return BusinessMapper.Mapper.Map<List<MovieDto>>(top10Movies);
        }

        public MovieDetailsExtendedDto GetMovieById(int id, ApplicationUser currentUser = null)
        {
            var movie = _uow.MovieRepository.GetMovieById(id);

            var movieDetailsExtendedDto = BusinessMapper.Mapper.Map<MovieDetailsExtendedDto>(movie);
            if (movieDetailsExtendedDto != null && currentUser != null)
            {
                var userVote = movie.VoteMovie.FirstOrDefault(p => p.ApplicationUser != null && p.ApplicationUser.Id == currentUser.Id);
                if (userVote != null)
                {
                    movieDetailsExtendedDto.CurrentUserVote = userVote.VoteScore;
                    movieDetailsExtendedDto.HasUserVoteActor = true;
                }
                movieDetailsExtendedDto.IsUserFavourite = movie.Favourites.Any(p => p.Id == currentUser.Id);
            }

            return movieDetailsExtendedDto;
        }

        public List<MovieDto> GetSimilarMoviesToMovieId(int movieId)
        {
            Recommendation.Initialize(_uow);
            var movie = _uow.MovieRepository.Get(movieId);
            var similarMovies = Recommendation.CompareMovieToMovie(movie);

            return BusinessMapper.Mapper.Map<List<MovieDto>>(similarMovies);
        }

        public List<MovieDto> GetTop100BestRatedMovies()
        {
            var movies = _uow.MovieRepository.GetTop100VotedMovies();

            return BusinessMapper.Mapper.Map<List<MovieDto>>(movies);
        }

        public int GetCountOfAllMovies()
        {
            var count = _uow.MovieRepository.GetAll().Count();
            return count;
        }

        public MovieDto GetMovieDtoById(int movieId)
        {
            var movie = _uow.MovieRepository.GetMovieById(movieId);

            return BusinessMapper.Mapper.Map<MovieDto>(movieId);
        }

        public List<MovieDetailsDto> SearchMovie(string searchString, SortMovieBy sortBy, SortOrder sortOrder)
        {
            var movieEntites = _uow.MovieRepository.SearchMovieByTitle(searchString);

            switch (sortBy)
            {
                case SortMovieBy.Duration:
                   movieEntites = sortOrder == SortOrder.Asc
                        ? movieEntites.OrderBy(p => p.Duration)
                            : movieEntites.OrderByDescending(p => p.Duration);
                    break;
                case SortMovieBy.Name:
                    movieEntites = sortOrder == SortOrder.Asc
                        ? movieEntites.OrderBy(p => p.TitleEng)
                        : movieEntites.OrderByDescending(p => p.TitleEng);
                    break;
                case SortMovieBy.PremiereDate:
                    movieEntites = sortOrder == SortOrder.Asc
                        ? movieEntites.OrderBy(p => p.PremiereDate)
                        : movieEntites.OrderByDescending(p => p.PremiereDate);
                    break;
            }

            return BusinessMapper.Mapper.Map<List<MovieDetailsDto>>(movieEntites);

        }

    }
}
