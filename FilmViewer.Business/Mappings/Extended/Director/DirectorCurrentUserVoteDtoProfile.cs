using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Director;

namespace FilmViewer.Business.Mappings.Extended.Director
{
    internal class DirectorCurrentUserVoteDtoProfile : Profile
    {
        public DirectorCurrentUserVoteDtoProfile()
        {
            CreateMap<DAL.Model.Director, DirectorCurrentUserVoteDto>()
                .IncludeBase<DAL.Model.Director, DirectorDetailsDto>()
                .ForMember(p => p.CurrentUserVote, opt => opt.Ignore())
                .ForMember(p => p.HasUserVoteDirector, opt => opt.Ignore());
        }
    }
}
