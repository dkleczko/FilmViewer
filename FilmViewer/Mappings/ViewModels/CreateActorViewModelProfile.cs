using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Models.Moderator;

namespace FilmViewer.Mappings.ViewModels
{
    internal class CreateActorViewModelProfile : Profile
    {
        public CreateActorViewModelProfile()
        {
            CreateMap<CreateActorViewModel, ActorDetailsDto>()
                .ForMember(p => p.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(p => p.Biography, opt => opt.MapFrom(x => x.Biography))
                .ForMember(p => p.BirthPlace, opt => opt.MapFrom(x => x.BirthPlace))
                .ForMember(p => p.Birthdate, opt => opt.MapFrom(x => x.Birthdate))
                .ForMember(p => p.MaritalStatus, opt => opt.MapFrom(x => x.MaritalStatus))
                .ForMember(p => p.VoteScores, opt => opt.Ignore())
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Folder, opt => opt.Ignore())
                .ForMember(p => p.PhotoUrl, opt => opt.Ignore())
                .ForMember(p => p.VoteCount, opt => opt.Ignore());

        }
    }
}