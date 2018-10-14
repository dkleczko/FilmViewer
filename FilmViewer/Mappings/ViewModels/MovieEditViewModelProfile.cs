using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;
using FilmViewer.Models.Moderator;

namespace FilmViewer.Mappings.ViewModels
{
    internal class MovieEditViewModelProfile : Profile
    {
        public MovieEditViewModelProfile()
        {
            CreateMap<MovieDetailsExtendedDto, MovieEditViewModel>()
                .ForMember(p => p.Director, opt => opt.MapFrom(x => new DirectorDto()
                {
                    Id = x.DirectorId,
                    Name = x.DirectorName
                }))
                .ForMember(p => p.Folder, opt => opt.MapFrom(x => x.Folder))
                .ForMember(p => p.MovieId, opt => opt.MapFrom(x => x.MovieId))
                .ForMember(p => p.Description, opt => opt.MapFrom(x => x.Content))
                .ForMember(p => p.Duration, opt => opt.MapFrom(x => x.Duration))
                .ForMember(p => p.HeraldUrl, opt => opt.MapFrom(x => x.HearldUrl))
                .ForMember(p => p.PremiereDate, opt => opt.MapFrom(x => x.PremiereDate))
                .ForMember(p => p.ProductionCountries, opt => opt.MapFrom(x => x.ProductionCountries))
                .ForMember(p => p.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(p => p.Actors, opt => opt.MapFrom(x => x.Actors))
                .ForMember(p => p.Categories, opt => opt.MapFrom(x => x.Categories))
                .ForMember(p => p.Photos, opt => opt.MapFrom(x => x.Photos))
                .ForAllOtherMembers(p => p.Ignore());

            CreateMap<MovieEditViewModel, MovieDetailsDto>()
                .ForMember(p => p.MovieId, opt => opt.MapFrom(x => x.MovieId))
                .ForMember(p => p.Content, opt => opt.MapFrom(x => x.Description))
                .ForMember(p => p.HearldUrl, opt => opt.MapFrom(x => x.HeraldUrl))
                .ForMember(p => p.PremiereDate, opt => opt.MapFrom(x => x.PremiereDate))
                .ForMember(p => p.ProductionCountries, opt => opt.MapFrom(x => x.ProductionCountries))
                .ForMember(p => p.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(p => p.Duration, opt => opt.MapFrom(x => x.Duration))
                .ForAllOtherMembers(p => p.Ignore());
        }
    }
}