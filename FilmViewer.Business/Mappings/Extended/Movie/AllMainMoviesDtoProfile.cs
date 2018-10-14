using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Extended.Movie
{
    internal class AllMainMoviesDtoProfile : Profile
    {
        public AllMainMoviesDtoProfile()
        {
            CreateMap<MainMovie, MainMoviesDto>()
                .ForMember(p => p.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.MovieId, opt => opt.MapFrom(x => x.Movie.Id))
                .ForMember(p => p.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(p => p.MovieImagePath, opt => opt.MapFrom(x => x.MovieImagePath))
                .ForMember(p => p.MovieTitle, opt => opt.MapFrom(x => x.Movie.TitleEng))
                .ForMember(p => p.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(p => p.SliderType, opt => opt.MapFrom(x => x.SliderType));
        }
    }
}
