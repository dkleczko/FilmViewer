using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;

namespace FilmViewer.Business.Mappings.Extended.Movie
{
    internal class MovieWithSimilarityDtoProfile : Profile
    {
        public MovieWithSimilarityDtoProfile()
        {
            CreateMap<DAL.Model.Movie, MovieWithSimilarityDto>()
                .IncludeBase<DAL.Model.Movie, MovieDto>()
                .ForMember(p => p.Similarity, opt => opt.MapFrom(x => x.similarity));
        }
    }
}
