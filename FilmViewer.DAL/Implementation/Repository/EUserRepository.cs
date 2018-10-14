using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EUserRepository : ERepository<ApplicationUser>, IUserRepository
    {
        public EUserRepository(FilmViewerDbContext context) : base(context)
        {
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }

        public ApplicationUser Get(string userId)
        {
            return FilmViewerDbContext.Users.FirstOrDefault(p => p.Id == userId);
        }

        public ApplicationUser GetApplicationUserWithVoteMoviePerson(string userId)
        {
            return FilmViewerDbContext.Users
                .Include(p => p.VoteMoviePerson)
                .FirstOrDefault(p => p.Id == userId);
        }

        public ApplicationUser GetApplicationUserWithFavourites(string userId)
        {
            return FilmViewerDbContext.Users
                .Include(p => p.Favourites)
                .FirstOrDefault(p => p.Id == userId);
        }

        public ApplicationUser GetApplicationUserWithVoteMovie(string userId)
        {
            return FilmViewerDbContext.Users
                .Include(p => p.VoteMovie.Select(x => x.ApplicationUser))
                .Include(p => p.VoteMovie.Select(x => x.Movie))
                .FirstOrDefault(p => p.Id == userId);
        }

        public int GetUserFavouritesCount(string userId)
        {
            return FilmViewerDbContext.Users
                .Include(p => p.Favourites)
                .Where(p => p.Id == userId)
                .Select(p => p.Favourites).Count();
        }

        public int GetUserMovieVotesCount(string userId)
        {
            return FilmViewerDbContext.Users
                .Include(p => p.VoteMovie)
                .Where(p => p.Id == userId)
                .Select(p => p.VoteMovie).Count();
        }

        public int GetUserActorVotesCount(string userId)
        {
            return FilmViewerDbContext.Users
                .Include(p => p.VoteMoviePerson)
                .Where(p => p.Id == userId)
                .Select(p => p.VoteMoviePerson).Count();
        }

        public IEnumerable<ApplicationUser> SearchUsersByUserName(string userName)
        {
            return FilmViewerDbContext.Users
                .Include(p => p.Roles)
                .Where(p => string.IsNullOrEmpty(userName) || p.UserName.ToUpper().Contains(userName.ToUpper()));
        }
    }
}
