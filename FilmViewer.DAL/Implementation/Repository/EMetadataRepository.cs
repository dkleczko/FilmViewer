using System.Collections.Generic;
using System.Linq;
using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EMetadataRepository : ERepository<Metadata>, IMetadataRepository
    {
        public EMetadataRepository(FilmViewerDbContext context) : base(context)
        {
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }

        public IEnumerable<Metadata> GetMetadatasByIds(List<int> metadataIds)
        {
            return FilmViewerDbContext.Metadatas
                .Where(p => metadataIds.Any() && metadataIds.Contains(p.Id));
        }

        public IEnumerable<Metadata> GetMetadatasBySearchString(string searchString)
        {
            return FilmViewerDbContext
                .Metadatas
                .Where(p => string.IsNullOrEmpty(searchString) || p.Name.Contains(searchString));
        }

        public bool IsMetadataWithThisNameAdded(string metadataName)
        {
            return FilmViewerDbContext
                .Metadatas
                .Any(p => p.Name == metadataName);
        }
    }
}
