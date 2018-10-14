using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class ApplicationUserDtoProfile : Profile
    {
        public ApplicationUserDtoProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(p => p.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(p => p.ApplicationUserId, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(p => p.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(p => p.LastName, opt => opt.MapFrom(x => x.LastName));
        }
    }
}
