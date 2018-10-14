using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Enums;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;

namespace FilmViewer.Business.DataProviders
{
    public class MetadataDataProvider : IMetadataDataProvider
    {
        private readonly IUnitOfWork _uow;
        public MetadataDataProvider(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<MetadataDto> GetMetadataBySearchString(string searchString)
        {
            var metadataEntitiesList = _uow.MetadataRepository.GetMetadatasBySearchString(searchString);

            return BusinessMapper.Mapper.Map<List<MetadataDto>>(metadataEntitiesList);
        }

        public List<MetadataDto> GetMetadataBySearchString(string searchString, SortOrder order)
        {
            var metadataEntitiesList = _uow.MetadataRepository.GetMetadatasBySearchString(searchString);
            switch (order)
            {
                case SortOrder.Asc:
                    metadataEntitiesList = metadataEntitiesList.OrderBy(p => p.Name);
                    break;
                case SortOrder.Desc:
                    metadataEntitiesList = metadataEntitiesList.OrderByDescending(p => p.Name);
                    break;
            }

            return BusinessMapper.Mapper.Map<List<MetadataDto>>(metadataEntitiesList);
        }

        public MetadataDto GetMetadataById(int metadataId)
        {
            var metadataEntity = _uow.MetadataRepository.Get(metadataId);

            return BusinessMapper.Mapper.Map<MetadataDto>(metadataEntity);
        }

        public bool IsMetadataWithThisNameAdded(string metadataName)
        {
            return _uow.MetadataRepository.IsMetadataWithThisNameAdded(metadataName);
        }
    }
}
