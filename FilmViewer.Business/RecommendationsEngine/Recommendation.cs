using System;
using System.Collections.Generic;
using System.Linq;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;
using Newtonsoft.Json;

namespace FilmViewer.Business.RecommendationsEngine
{
    public static class Recommendation
    {
        private static  IUnitOfWork _uow;
        public static void Initialize(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public static List<Movie> PrepareMovieForUser(ApplicationUser user, ViewType viewType)
        {
            var userVector = PrepareUserVector(user);
            userVector = userVector.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
            var userVectorList = userVector.Take(20).Select(p => p.Key).ToList();
            var bestMovies = CompareUserVectorToMovie(userVectorList, user);
            if (viewType == ViewType.MainView)
            {
                return bestMovies.Take(10).ToList();
            }
            else if(viewType == ViewType.AllRecomendations)
            {
                return bestMovies.Where(p => p.similarity >=0.5).ToList();

            }
            return bestMovies;
          }
        private static Dictionary<string, double> PrepareUserVector(ApplicationUser user)
        {
            Dictionary<string, double> userVector = new Dictionary<string, double>();
            var voteMovies = user.VoteMovie.ToList();
            foreach (var voteMovie in voteMovies)
            {
                var metadataModel = GetMetadataModelFromString(voteMovie.Movie.Metadata);
                userVector = CreateDataToUserVector(metadataModel, userVector, voteMovie.VoteScore);

            }
            return userVector;

        }
        private static Dictionary<string, double> CreateDataToUserVector(List<string> metadatas, Dictionary<string, double> userVector, int vote)
        {
            foreach (var metadata in metadatas)
            {
                if (userVector.ContainsKey(metadata))
                {
                    userVector[metadata] += vote;
                }
                else
                {
                    userVector.Add(metadata, 1);
                }
            }
            return userVector;
        }


        public static List<Movie> CompareMovieToMovie(Movie movie)
        {
            var movies = _uow.MovieRepository.GetAllMoviesExceptIds(new List<int>()
            {
                movie.Id
            });
            var sourceMetadataModel = GetMetadataModelFromString(movie.Metadata);
            foreach (var m in movies)
            {
                var outMetadataModel = GetMetadataModelFromString(m.Metadata);
                var similarity = Compare(sourceMetadataModel, outMetadataModel);
                m.similarity = similarity;
            }
            var bestMovies = movies.OrderByDescending(p => p.similarity).Take(10).ToList();
            return bestMovies;

        }
        private static List<Movie> CompareUserVectorToMovie(List<string> userVector, ApplicationUser user)
        {
            MovieEqualityComparer movieComparer = new MovieEqualityComparer();
            var userVotesList = user.VoteMovie.Select(p => p.Movie).ToList();
            var userLikeList = user.Favourites.ToList();
            var moviesList = _uow.MovieRepository.GetAllMoviesWithMetadata();
            moviesList = moviesList.Except(userVotesList, movieComparer).Except(userLikeList, movieComparer)
                .ToList();
            foreach (var movie in moviesList)
            {
                var outMetadataModel = GetMetadataModelFromString(movie.Metadata);
                var similarity = Compare(userVector, outMetadataModel);
                movie.similarity = similarity;
            }
            var bestMovies = moviesList.OrderByDescending(p => p.similarity).ToList();
            return bestMovies;
        }
        private static List<string> GetMetadataModelFromString(string source)
        {
            return JsonConvert.DeserializeObject<List<string>>(source);

        }
        public static double Compare(List<string> source, List<string> itemsList)
        {
            double similarity = 0;
            foreach (var s in source)
            {
                if (itemsList.Contains(s))
                    similarity++;
            }

            double result = similarity / (Math.Sqrt(source.Count) + Math.Sqrt(itemsList.Count));

            return result;
        }



    }

    public class MovieEqualityComparer : IEqualityComparer<Movie>
    {

        public bool Equals(Movie x, Movie y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null | y == null)
                return false;
            else if (x.Id == y.Id)
                return true;
            else
                return false;
        }

        public int GetHashCode(Movie obj)
        {
            int hCode = obj.Id;
            return hCode.GetHashCode();
        }
    }

}