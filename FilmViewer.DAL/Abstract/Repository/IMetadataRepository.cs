using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IMetadataRepository : IRepository<Metadata>
    {
        IEnumerable<Metadata> GetMetadatasByIds(List<int> metadataIds);
        IEnumerable<Metadata> GetMetadatasBySearchString(string searchString);
        bool IsMetadataWithThisNameAdded(string metadataName);

    }
}
