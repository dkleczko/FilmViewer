using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Models.Moderator;

namespace FilmViewer.Mappings.ViewModels
{
    internal class EditCategoryViewModelProfile : Profile
    {
        public EditCategoryViewModelProfile()
        {
            CreateMap<CategoryDto, EditCategoryViewModel>()
                .ForMember(p => p.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ForMember(p => p.CategoryName, opt => opt.MapFrom(x => x.CategoryName));

            CreateMap<EditCategoryViewModel, CategoryDto>()
                .ForMember(p => p.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ForMember(p => p.CategoryName, opt => opt.MapFrom(x => x.CategoryName));

        }
    }
}