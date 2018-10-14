using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EActorRepository : ERepository<Actor>, IActorRepository
    {
        public EActorRepository(FilmViewerDbContext ctx) : base(ctx)
        {
            
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }


        public IEnumerable<Actor> GetActorsBySearchString(string searchString)
        {
            return FilmViewerDbContext.Actors
                .Include(p => p.Votes)
                .Where(p => string.IsNullOrEmpty(searchString) 
                || p.Name.ToUpper().Contains(searchString.ToUpper()));
        }

        public Actor GetActorByIdWithPhotosMoviesVoteActor(int id)
        {
            return FilmViewerDbContext.Actors
                .Include(p => p.ConnectedMovies)
                .Include(p => p.PhotoUrls)
                .Include(p => p.Votes)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Actor> GetTop100VotedActors()
        {
            return FilmViewerDbContext.Actors
                .Where(p => p.VoteCount > 0)
                .OrderByDescending(p => p.VoteScores / p.VoteCount).Take(100);
        }

        public IEnumerable<Actor> GetActorsByListOfIds(List<int> actorIds)
        {
            return FilmViewerDbContext.Actors
                .Where(p => actorIds.Any() && actorIds.Contains(p.Id));
        }
    }
}