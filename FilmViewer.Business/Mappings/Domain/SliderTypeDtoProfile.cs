using AutoMapper;
using FilmViewer.Business.Dto.Domain.Enum;
using FilmViewer.DAL.Model.Enum;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class SliderTypeDtoProfile : Profile
    {
        public SliderTypeDtoProfile()
        {
            CreateMap<SliderType, SliderTypeDto>();

            CreateMap<SliderTypeDto, SliderType>();
        }
    }
}
