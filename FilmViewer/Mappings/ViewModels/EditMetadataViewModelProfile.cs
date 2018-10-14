using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Models.Moderator;

namespace FilmViewer.Mappings.ViewModels
{
    internal class EditMetadataViewModelProfile : Profile
    {
        public EditMetadataViewModelProfile()
        {
            CreateMap<MetadataDto, EditMetadataViewModel>()
                .ForMember(p => p.MetadataId, opt => opt.MapFrom(x => x.MetadataId))
                .ForMember(p => p.MetadataName, opt => opt.MapFrom(x => x.MetadataName));

            CreateMap<EditMetadataViewModel, MetadataDto>()
                .ForMember(p => p.MetadataId, opt => opt.MapFrom(x => x.MetadataId))
                .ForMember(p => p.MetadataName, opt => opt.MapFrom(x => x.MetadataName));
        }
    }
}