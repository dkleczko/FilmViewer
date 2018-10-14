using AutoMapper;
using FilmViewer.Business.Dto.Extended.Director;

namespace FilmViewer.Business.Mappings.Extended.Director
{
    internal class DirectorPhotosMoviesDtoProfile :Profile
    {
        public DirectorPhotosMoviesDtoProfile()
        {
            CreateMap<DAL.Model.Director, DirectorPhotosMoviesDto>()
                .IncludeBase<DAL.Model.Director, DirectorDetailsPhotosDto>()
                .ForMember(p => p.Movies, opt => opt.MapFrom(x => x.ConnectedMovies))
                .ForMember(p => p.CurrentUserVote, opt => opt.Ignore())
                .ForMember(p => p.HasUserVoteDirector, opt => opt.Ignore());
        }
    }
}
