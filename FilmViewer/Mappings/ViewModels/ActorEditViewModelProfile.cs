using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.Models.Moderator;

namespace FilmViewer.Mappings.ViewModels
{
    internal class ActorEditViewModelProfile : Profile
    {
        public ActorEditViewModelProfile()
        {
            CreateMap<ActorPhotosMoviesDto, ActorEditViewModel>()
                .ForMember(p => p.Folder, opt => opt.MapFrom(x => x.Folder))
                .ForMember(p => p.ActorId, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(p => p.Biography, opt => opt.MapFrom(x => x.Biography))
                .ForMember(p => p.BirthPlace, opt => opt.MapFrom(x => x.BirthPlace))
                .ForMember(p => p.Birthdate, opt => opt.MapFrom(x => x.Birthdate))
                .ForMember(p => p.MaritalStatus, opt => opt.MapFrom(x => x.MaritalStatus))
                .ForMember(p => p.Photos, opt => opt.MapFrom(x => x.Photos));

            CreateMap<ActorEditViewModel, ActorDetailsDto>()
                .ForMember(p => p.Id, opt => opt.MapFrom(x => x.ActorId))
                .ForMember(p => p.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(p => p.Biography, opt => opt.MapFrom(x => x.Biography))
                .ForMember(p => p.BirthPlace, opt => opt.MapFrom(x => x.BirthPlace))
                .ForMember(p => p.Birthdate, opt => opt.MapFrom(x => x.Birthdate))
                .ForMember(p => p.MaritalStatus, opt => opt.MapFrom(x => x.MaritalStatus))
                .ForAllOtherMembers(p => p.Ignore());
        }
    }
}