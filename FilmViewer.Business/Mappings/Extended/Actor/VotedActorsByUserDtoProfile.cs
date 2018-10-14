using AutoMapper;
using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Extended.Actor
{
    internal class VotedActorsByUserDtoProfile :Profile
    {
        public VotedActorsByUserDtoProfile()
        {
            CreateMap<VoteMoviePerson, VotedActorsByUserDto>()
                .ForMember(p => p.CurrentVoteScore, opt => opt.MapFrom(x => x.VoteScore))
                .ForMember(p => p.Actor, opt => opt.MapFrom(x => x.MoviePerson));
        }
    }
}
