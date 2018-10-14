using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class PhotoPathDtoProfile : Profile
    {
        public PhotoPathDtoProfile()
        {
            CreateMap<PhotoPath, PhotoPathDto>()
                .ForMember(p => p.PhotoId, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.Path, opt => opt.MapFrom(x => x.Path));

            CreateMap<PhotoPathDto, PhotoPath>()
                .ForMember(p => p.Id, opt => opt.MapFrom(x => x.PhotoId))
                .ForMember(p => p.Path, opt => opt.MapFrom(x => x.Path));
        }
    }
}
