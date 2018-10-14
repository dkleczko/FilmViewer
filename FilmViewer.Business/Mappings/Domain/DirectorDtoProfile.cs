using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class DirectorDtoProfile : Profile
    {
        public DirectorDtoProfile()
        {
            CreateMap<Director, DirectorDto>()
                .IncludeBase<MoviePerson, MoviePersonDto>();

            CreateMap<DirectorDto, Director>()
                .IncludeBase<MoviePersonDto, MoviePerson>();
        }
    }
}
