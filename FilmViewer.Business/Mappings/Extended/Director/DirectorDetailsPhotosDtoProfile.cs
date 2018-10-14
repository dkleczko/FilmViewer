using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Director;

namespace FilmViewer.Business.Mappings.Extended.Director
{
    internal class DirectorDetailsPhotosDtoProfile : Profile
    {
        public DirectorDetailsPhotosDtoProfile()
        {
            CreateMap<DAL.Model.Director, DirectorDetailsPhotosDto>()
                .IncludeBase<DAL.Model.Director, DirectorDetailsDto>()
                .ForMember(p => p.Photos, opt => opt.MapFrom(x => x.PhotoUrls));
        }
    }
}
