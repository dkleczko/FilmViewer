using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;

namespace FilmViewer.Business.Mappings.Extended.Actor
{
    internal class ActorPhotosMoviesDtoProfile : Profile
    {
        public ActorPhotosMoviesDtoProfile()
        {
            CreateMap<DAL.Model.Actor, ActorPhotosMoviesDto>()
                .IncludeBase<DAL.Model.Actor, ActorDetailsDto>()
                .ForMember(p => p.Photos, opt => opt.MapFrom(x => x.PhotoUrls))
                .ForMember(p => p.Movies, opt => opt.MapFrom(x => x.ConnectedMovies))
                .ForMember(p => p.CurrentUserVote, opt => opt.Ignore())
                .ForMember(p => p.HasUserVoteActor, opt => opt.Ignore());

        }

    }
}
