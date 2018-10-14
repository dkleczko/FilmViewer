using AutoMapper;
using FilmViewer.Business.Dto.Extended.Director;

namespace FilmViewer.Business.Mappings.Extended.Director
{
    internal class CurrentDirectorVoteDtoProfile :Profile
    {
        public CurrentDirectorVoteDtoProfile()
        {
            CreateMap<DAL.Model.Director, CurrentDirectorVoteDto>()
                .ForMember(p => p.VoteCount, opt => opt.MapFrom(x => x.VoteCount))
                .ForMember(p => p.Score, opt => opt.MapFrom(x => x.VoteScores));
        }
    }
}
