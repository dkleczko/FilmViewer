using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class MovieDetailsDtoProfile : Profile
    {
        public MovieDetailsDtoProfile()
        {
            CreateMap<Movie, MovieDetailsDto>()
                .IncludeBase<Movie, MovieDto>()
                .ForMember(p => p.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(p => p.Duration, opt => opt.MapFrom(x => x.Duration))
                .ForMember(p => p.HearldUrl, opt => opt.MapFrom(x => x.HeraldUrl))
                .ForMember(p => p.PremiereDate, opt => opt.MapFrom(x => x.PremiereDate))
                .ForMember(p => p.ProductionCountries, opt => opt.MapFrom(x => x.ProductionCountries));

        }
    }
}
