using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;

namespace FilmViewer.Business.Mappings.Extended.Movie
{
    internal class MovieDetailsExtendedDtoProfile : Profile
    {
        public MovieDetailsExtendedDtoProfile()
        {
            CreateMap<DAL.Model.Movie, MovieDetailsExtendedDto>()
                .IncludeBase<DAL.Model.Movie, MovieDetailsDto>()
                .ForMember(p => p.DirectorId, opt => opt.MapFrom(x => x.Director.Id))
                .ForMember(p => p.DirectorName, opt => opt.MapFrom(x => x.Director.Name))
                .ForMember(p => p.Categories, opt => opt.MapFrom(x => x.Categories))
                .ForMember(p => p.Actors, opt => opt.MapFrom(x => x.Actors))
                .ForMember(p => p.CommentsCount, opt => opt.MapFrom(x => x.Comments.Count))
                .ForMember(p => p.Photos, opt => opt.MapFrom(x => x.PhotoUrls))
                .ForMember(p => p.IsUserFavourite, opt => opt.Ignore())
                .ForMember(p => p.CurrentUserVote, opt => opt.Ignore())
                .ForMember(p => p.HasUserVoteActor, opt => opt.Ignore());


        }
    }
}
