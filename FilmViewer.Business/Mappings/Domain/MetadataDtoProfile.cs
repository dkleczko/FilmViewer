using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class MetadataDtoProfile: Profile
    {
        public MetadataDtoProfile()
        {
            CreateMap<Metadata, MetadataDto>()
                .ForMember(p => p.MetadataId, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.MetadataName, opt => opt.MapFrom(x => x.Name));

            CreateMap<MetadataDto, Metadata>()
                .ForMember(p => p.Id, opt => opt.MapFrom(x => x.MetadataId))
                .ForMember(p => p.Name, opt => opt.MapFrom(x => x.MetadataName));
        }
    }
}
