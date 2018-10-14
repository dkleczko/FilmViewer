using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;

namespace FilmViewer.Business.Mappings.Extended.Movie
{
    internal class RecommendedMovieForUserDtoProfile : Profile
    {
        public RecommendedMovieForUserDtoProfile()
        {
            CreateMap<DAL.Model.Movie, RecommendedMovieForUserDto>()
                .IncludeBase<DAL.Model.Movie, MovieDto>()
                .ForMember(p => p.Duration, opt => opt.MapFrom(x => x.Duration))
                .ForMember(p => p.PremiereDate, opt => opt.MapFrom(x => x.PremiereDate))
                .ForMember(p => p.Categories, opt => opt.MapFrom(x => x.Categories));
        }
    }
}
