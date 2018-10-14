using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser Get(string userId);
        ApplicationUser GetApplicationUserWithVoteMoviePerson(string userId);
        ApplicationUser GetApplicationUserWithFavourites(string userId);
        ApplicationUser GetApplicationUserWithVoteMovie(string userId);
        int GetUserFavouritesCount(string userId);
        int GetUserMovieVotesCount(string userId);
        int GetUserActorVotesCount(string userId);
        IEnumerable<ApplicationUser> SearchUsersByUserName(string userName);
    }
}
