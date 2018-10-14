using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Enums;

namespace FilmViewer.Business.Abstract.Services
{
    public interface IMetadataService
    {
        AddResult AddMetadata(MetadataDto metadataDto);
        void DeleteMetadata(int metadataId);
        AddResult EditMetadata(MetadataDto metadataDto);
    }
}
