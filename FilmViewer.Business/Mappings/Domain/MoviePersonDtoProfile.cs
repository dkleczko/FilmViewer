using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class MoviePersonDtoProfile : Profile
    {
        public MoviePersonDtoProfile()
        {
            CreateMap<MoviePerson, MoviePersonDto>()
                .ForMember(p => p.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(p => p.Folder, opt => opt.MapFrom(x => x.Folder))
                .ForMember(p => p.PhotoUrl, opt => opt.MapFrom(x => x.PhotoUrl))
                .ForMember(p => p.VoteCount, opt => opt.MapFrom(x => x.VoteCount))
                .ForMember(p => p.VoteScores, opt => opt.MapFrom(x => x.VoteScores));

            CreateMap<MoviePersonDto, MoviePerson>()
                .ForMember(p => p.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.VoteScores, opt => opt.MapFrom(x => x.VoteScores))
                .ForMember(p => p.Folder, opt => opt.MapFrom(x => x.Folder))
                .ForMember(p => p.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(p => p.PhotoUrl, opt => opt.MapFrom(x => x.PhotoUrl))
                .ForMember(p => p.VoteCount, opt => opt.MapFrom(x => x.VoteCount))
                .ForMember(p => p.Birthdate, opt => opt.Ignore())
                .ForMember(p => p.BirthPlace, opt => opt.Ignore())
                .ForMember(p => p.Biography, opt => opt.Ignore())
                .ForMember(p => p.PhotoUrls, opt => opt.Ignore())
                .ForMember(p => p.Similarity, opt => opt.Ignore())
                .ForMember(p => p.ConnectedMovies, opt => opt.Ignore())
                .ForMember(p => p.Votes, opt => opt.Ignore())
                .ForMember(p => p.MaritalStatus, opt => opt.Ignore());
        }
    }
}
