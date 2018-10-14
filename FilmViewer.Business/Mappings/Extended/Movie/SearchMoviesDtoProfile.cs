using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;

namespace FilmViewer.Business.Mappings.Extended.Movie
{
    internal class SearchMoviesDtoProfile : Profile
    {
        public SearchMoviesDtoProfile()
        {
            CreateMap<DAL.Model.Movie, SearchMoviesDto>()
                .IncludeBase<DAL.Model.Movie, MovieDetailsDto>()
                .ForMember(p => p.CommentsCount, opt => opt.MapFrom(x => x.Comments.Count))
                .ForMember(p => p.VideosCount, opt => opt.MapFrom(x => 1))
                .ForMember(p => p.PhotosCount, opt => opt.MapFrom(x => x.PhotoUrls.Count))
                .ForMember(p => p.Categories, opt => opt.MapFrom(x => x.Categories))
                .ForMember(p => p.DirectorName, opt => opt.MapFrom(x => x.Director.Name))
                .ForMember(p => p.DirectorId, opt => opt.MapFrom(x => x.Director.Id))
                .ForMember(p => p.IsUserFavourite, opt => opt.Ignore())
                .ForMember(p => p.CurrentUserVote, opt => opt.Ignore())
                .ForMember(p => p.HasUserVoteActor, opt => opt.Ignore());
        }
    }
}
