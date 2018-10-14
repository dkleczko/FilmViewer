using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Models.Moderator;

namespace FilmViewer.Mappings.ViewModels
{
    internal class CreateMetadataViewModelProfile : Profile
    {
        public CreateMetadataViewModelProfile()
        {
            CreateMap<CreateMetadataViewModel, MetadataDto>()
                .ForMember(p => p.MetadataName, opt => opt.MapFrom(x => x.Name))
                .ForMember(p => p.MetadataId, opt => opt.Ignore());
        }
    }
}