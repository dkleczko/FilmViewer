using System.Collections.Generic;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Abstract.Helpers
{
    public interface IMetadataHelper
    {
        IEnumerable<string> GenerateMetadataList(IEnumerable<Metadata> metadatas, Movie movie);
    }
}
