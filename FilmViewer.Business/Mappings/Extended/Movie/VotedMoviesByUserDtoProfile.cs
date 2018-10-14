using AutoMapper;
using FilmViewer.Business.Dto.Extended.Movie;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Extended.Movie
{
    internal class VotedMoviesByUserDtoProfile : Profile
    {
        public VotedMoviesByUserDtoProfile()
        {
            CreateMap<VoteMovie, VotedMoviesByUserDto>()
                .ForMember(p => p.CurrentVoteScore, opt => opt.MapFrom(x => x.VoteScore))
                .ForMember(p => p.Movie, opt => opt.MapFrom(x => x.Movie));
        }
    }
}
