using AutoMapper;
using FilmViewer.Business.Dto.Extended.Director;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Extended.Director
{
    internal class VotedDirectorByUserDtoProfile : Profile
    {
        public VotedDirectorByUserDtoProfile()
        {
            CreateMap<VoteMoviePerson, VotedDirectorByUserDto>()
                .ForMember(p => p.Director, opt => opt.MapFrom(x => x.MoviePerson))
                .ForMember(p => p.CurrentUserVoteScore, opt => opt.MapFrom(x => x.VoteScore));
        }
    }
}
