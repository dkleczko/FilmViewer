using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;

namespace FilmViewer.Business.Mappings.Extended.Actor
{
    internal class ActorDetailsCurrentUserVoteDtoProfile : Profile
    {
        public ActorDetailsCurrentUserVoteDtoProfile()
        {
            CreateMap<DAL.Model.Actor, ActorCurrentUserVoteDto>()
                .IncludeBase<DAL.Model.Actor, ActorDetailsDto>()
                .ForMember(p => p.CurrentUserVote, opt => opt.Ignore())
                .ForMember(p => p.HasUserVoteActor, opt => opt.Ignore());
        }
    }
}
