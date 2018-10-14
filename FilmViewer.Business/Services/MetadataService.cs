using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Enums;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Services
{
    public class MetadataService : IMetadataService
    {
        private readonly IUnitOfWork _uow;

        public MetadataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public AddResult AddMetadata(MetadataDto metadataDto)
        {
            var isAlreadyMetadataWithThisName =
                _uow.MetadataRepository.IsMetadataWithThisNameAdded(metadataDto.MetadataName);

            if (!isAlreadyMetadataWithThisName)
            {
                var metadataEntity = BusinessMapper.Mapper.Map<Metadata>(metadataDto);

                _uow.MetadataRepository.Add(metadataEntity);

                var comp = _uow.Complete();
                return comp > 0 ? AddResult.Added : AddResult.UnknownError;

            }

            return AddResult.DuplicateElement;
        }

        public void DeleteMetadata(int metadataId)
        {
            var metadataEntity = _uow.MetadataRepository.Get(metadataId);

            _uow.MetadataRepository.Remove(metadataEntity);

            _uow.Complete();
        }

        public AddResult EditMetadata(MetadataDto metadataDto)
        {
            var isAlreadyMetadataWithThisName =
                _uow.MetadataRepository.IsMetadataWithThisNameAdded(metadataDto.MetadataName);
            if (!isAlreadyMetadataWithThisName)
            {
                var metadataEntity = _uow.MetadataRepository.Get(metadataDto.MetadataId);

                metadataEntity.Name = metadataDto.MetadataName;

                var com = _uow.Complete();

                return com > 0   ? AddResult.Added : AddResult.UnknownError;

            }

            return AddResult.DuplicateElement;

        }
    }
}
