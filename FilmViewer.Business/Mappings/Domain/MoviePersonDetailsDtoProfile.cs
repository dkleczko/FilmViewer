using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class MoviePersonDetailsDtoProfile : Profile
    {
        public MoviePersonDetailsDtoProfile()
        {
            CreateMap<MoviePerson, MoviePersonDetailsDto>()
                .IncludeBase<MoviePerson, MoviePersonDto>()
                .ForMember(p => p.Biography, opt => opt.MapFrom(x => x.Biography))
                .ForMember(p => p.BirthPlace, opt => opt.MapFrom(x => x.BirthPlace))
                .ForMember(p => p.Birthdate, opt => opt.MapFrom(x => x.Birthdate))
                .ForMember(p => p.MaritalStatus, opt => opt.MapFrom(x => x.MaritalStatus));

            CreateMap<MoviePersonDetailsDto, MoviePerson>()
                .IncludeBase<MoviePersonDto, MoviePerson>()
                .ForMember(p => p.BirthPlace, opt => opt.MapFrom(x => x.BirthPlace))
                .ForMember(p => p.Birthdate, opt => opt.MapFrom(x => x.Birthdate))
                .ForMember(p => p.MaritalStatus, opt => opt.MapFrom(x => x.MaritalStatus))
                .ForMember(p => p.Biography, opt => opt.MapFrom(x => x.Biography))
                .ForMember(p => p.PhotoUrls, opt => opt.Ignore())
                .ForMember(p => p.Similarity, opt => opt.Ignore())
                .ForMember(p => p.ConnectedMovies, opt => opt.Ignore())
                .ForMember(p => p.Votes, opt => opt.Ignore());
        }
    }
}
