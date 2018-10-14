using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class ApplicationUserDetailsDtoProfile : Profile
    {
        public ApplicationUserDetailsDtoProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDetailsDto>()
                .IncludeBase<ApplicationUser, ApplicationUserDto>()
                .ForMember(p => p.IsLockedOutEnabled, opt => opt.MapFrom(x => x.LockoutEnabled))
                .ForMember(p => p.LockoutEndDateUtc, opt=> opt.MapFrom(x => x.LockoutEndDateUtc))
                .ForMember(p => p.Roles, opt => opt.Ignore());
        }
    }
}
