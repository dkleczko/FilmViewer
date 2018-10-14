using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;

namespace FilmViewer.Business.Mappings.Extended.Movie
{
    internal class Top6RatesMoviesDtoProfile : Profile
    {
        public Top6RatesMoviesDtoProfile()
        {
            CreateMap<DAL.Model.Movie, Top6RatesMoviesDto>()
                .IncludeBase<DAL.Model.Movie, MovieDto>()
                .ForMember(p => p.CommentsCount, opt => opt.MapFrom(x => x.Comments.Count))
                .ForMember(p => p.Duration, opt => opt.MapFrom(x => x.Duration));


        }
    }
}
