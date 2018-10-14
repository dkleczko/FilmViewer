using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class RoleDtoProfile : Profile
    {
        public RoleDtoProfile()
        {
            CreateMap<IdentityRole, RoleDto>()
                .ForMember(p => p.RoleId, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.RoleName, opt => opt.MapFrom(x => x.Name));
        }
    }
}
