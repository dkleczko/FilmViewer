using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.Business.Dto.Extended.Director;
using FilmViewer.Business.Dto.Extended.Movie;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface IUserDataProvider
    {
        int GetUserFavouritesCount(string userId);
        int GetUserMovieVotesCount(string userId);
        int GetUserActorVotesCount(string userId);
        List<VotedMoviesByUserDto> GetVotedMoviesByUser(string userId);
        List<VotedActorsByUserDto> GetVotesActorsByUser(string userId);
        List<MovieDto> GetFavouritesMoviesByUser(string userId);
        List<MovieWithSimilarityDto> GetRecommendedMoviesWithSimilarity(ApplicationUser user);
        List<VotedDirectorByUserDto> GetVotedDirectorsByUser(string userId);
        List<ApplicationUserDetailsDto> SearchUsersByUsername(string searchString = "");
        ApplicationUserDetailsDto GetApplicationUserDetailsById(string id);
    }
}
