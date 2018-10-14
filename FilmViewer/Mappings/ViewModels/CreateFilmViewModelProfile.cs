using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Models.Moderator;

namespace FilmViewer.Mappings.ViewModels
{
    internal class CreateFilmViewModelProfile : Profile
    {
        public CreateFilmViewModelProfile()
        {
            CreateMap<CreateFilmViewModel, MovieDetailsDto>()
                .ForMember(p => p.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(p => p.Content, opt => opt.MapFrom(x => x.Description))
                .ForMember(p => p.Duration, opt => opt.MapFrom(x => x.Duration))
                .ForMember(p => p.PremiereDate, opt => opt.MapFrom(x => x.PremiereDate))
                .ForMember(p => p.ProductionCountries, opt => opt.MapFrom(x => x.ProductionCountries))
                .ForMember(p => p.HearldUrl, opt => opt.MapFrom(x => x.HeraldUrl))
                .ForMember(p => p.MovieId, opt => opt.Ignore())
                .ForMember(p => p.Folder, opt => opt.Ignore())
                .ForMember(p => p.PhotoUrl, opt => opt.Ignore())
                .ForMember(p => p.VoteScores, opt => opt.Ignore())
                .ForMember(p => p.VoteCount, opt => opt.Ignore());
        }
    }
}