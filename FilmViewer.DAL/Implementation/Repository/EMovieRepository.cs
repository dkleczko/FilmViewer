using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EMovieRepository : ERepository<Movie>, IMovieRepository
    {
        public IEnumerable<Movie> GetTop6RatesMovies()
        {
            return FilmViewerDbContext.Movies.OrderByDescending(p => p.VoteScores);
        }

        public IEnumerable<Movie> Get10RandomMovies()
        {
            return FilmViewerDbContext.Movies.OrderBy(p => Guid.NewGuid()).Take(10);
        }

        public IEnumerable<Movie> GetAllMoviesExceptIds(List<int> movieIds)
        {
            movieIds = movieIds ?? new List<int>();
            return FilmViewerDbContext.Movies
                .Include(p => p.Categories)
                .Where(p => !movieIds.Any() || !movieIds.Contains(p.Id));
        }

        public IEnumerable<Movie> GetAllMoviesWithMetadata()
        {
            return FilmViewerDbContext.Movies;
        }

        public IEnumerable<Movie> SearchMovieByTitle(string searchString)
        {
            return FilmViewerDbContext.Movies
                .Include(p => p.Categories)
                .Include(p => p.PhotoUrls)
                .Include(p => p.Director)
                .Include(p => p.Comments)
                .Include(p => p.Favourites)
                .Include(p => p.VoteMovie)
                .Where(p => string.IsNullOrEmpty(searchString) ||
                            p.TitleEng.ToUpper().Contains(searchString.ToUpper()));
        }

        public IEnumerable<Movie> SearchMovieByDirectorName(string searchString)
        {
            return FilmViewerDbContext.Movies
                .Include(p => p.Categories)
                .Include(p => p.PhotoUrls)
                .Include(p => p.Director)
                .Include(p => p.Comments)
                .Include(p => p.Favourites)
                .Include(p => p.VoteMovie)
                .Where(p => string.IsNullOrEmpty(searchString) ||
                            (p.Director != null  && p.Director.Name.ToUpper().Contains(searchString.ToUpper())));
        }

        public IEnumerable<Movie> SearchMovieByActorName(string searchString)
        {
            return FilmViewerDbContext.Movies
                .Include(p => p.Categories)
                .Include(p => p.PhotoUrls)
                .Include(p => p.Director)
                .Include(p => p.Comments)
                .Include(p => p.Actors)
                .Include(p => p.Favourites)
                .Include(p => p.VoteMovie)
                .Where(p => string.IsNullOrEmpty(searchString) ||
                            (p.Actors != null && p.Actors.Any(x => x.Name.ToUpper().Contains(searchString.ToUpper()))));
        }

        public IEnumerable<Movie> SearchMovieByCategoryId(int categoryId)
        {
            return FilmViewerDbContext.Movies
                .Include(p => p.Categories)
                .Include(p => p.PhotoUrls)
                .Include(p => p.Director)
                .Include(p => p.Comments)
                .Include(p => p.Favourites)
                .Include(p => p.VoteMovie)
                .Where(p => p.Categories != null && p.Categories.Any(x => x.Id == categoryId));
        }

        public IEnumerable<Movie> GetTop10MostCommentedMovies()
        {
            return FilmViewerDbContext.Movies
                .OrderByDescending(p => p.Comments.Count).Take(10);
        }

        public Movie GetMovieById(int movieId)
        {
            return FilmViewerDbContext.Movies
                .Include(p => p.Actors)
                .Include(p => p.Categories)
                .Include(p => p.Favourites)
                .Include(p => p.PhotoUrls)
                .Include(p => p.VoteMovie)
                .Include(p => p.Director)
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == movieId);
        }

        public IEnumerable<Movie> GetTop100VotedMovies()
        {
            return FilmViewerDbContext.Movies
                    .Where(p => p.VoteCount >0)
                .OrderByDescending(p => p.VoteScores / p.VoteCount).Take(100);
        }

        public Movie GetMovieByIdWithPhotos(int movieId)
        {
            return FilmViewerDbContext
                .Movies
                .FirstOrDefault(p => p.Id == movieId);
        }


        public EMovieRepository(FilmViewerDbContext context) : base(context)
        {
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }
        
    }
}