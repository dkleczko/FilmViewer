using AutoMapper;
using FilmViewer.Business.Dto.Extended.Actor;

namespace FilmViewer.Business.Mappings.Extended.Actor
{
    internal class CurrentActorVoteDtoProfile : Profile
    {
        public CurrentActorVoteDtoProfile()
        {
            CreateMap<DAL.Model.Actor, CurrentActorVoteDto>()
                .ForMember(p => p.VoteCount, opt => opt.MapFrom(x => x.VoteCount))
                .ForMember(p => p.Score, opt => opt.MapFrom(x => x.VoteScores));
        }
    }
}
