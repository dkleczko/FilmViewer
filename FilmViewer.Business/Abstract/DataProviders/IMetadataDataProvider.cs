using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Enums;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface IMetadataDataProvider
    {
        List<MetadataDto> GetMetadataBySearchString(string searchString);
        bool IsMetadataWithThisNameAdded(string metadataName);
        List<MetadataDto> GetMetadataBySearchString(string searchString, SortOrder order);
        MetadataDto GetMetadataById(int metadataId);
    }
}
