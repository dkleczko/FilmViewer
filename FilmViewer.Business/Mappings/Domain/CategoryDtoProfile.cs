using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class CategoryDtoProfile : Profile
    {
        public CategoryDtoProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(p => p.CategoryName, opt => opt.MapFrom(x => x.Name))
                .ForMember(p => p.CategoryId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
