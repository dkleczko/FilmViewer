using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class MovieDtoProfile : Profile
    {
        public MovieDtoProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(p => p.VoteScores, opt => opt.MapFrom(x => x.VoteScores))
                .ForMember(p => p.Folder, opt => opt.MapFrom(x => x.Folder))
                .ForMember(p => p.MovieId, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.PhotoUrl, opt => opt.MapFrom(x => x.PhotoPath))
                .ForMember(p => p.Title, opt => opt.MapFrom(x => x.TitleEng))
                .ForMember(p => p.VoteCount, opt => opt.MapFrom(x => x.VoteCount));

        }
    }
}
