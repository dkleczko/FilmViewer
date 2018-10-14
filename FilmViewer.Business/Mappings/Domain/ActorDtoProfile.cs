using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class ActorDtoProfile : Profile
    {
        public ActorDtoProfile()
        {
            CreateMap<Actor, ActorDto>()
                .IncludeBase<MoviePerson, MoviePersonDto>();

            CreateMap<ActorDto, Actor>()
                .IncludeBase<MoviePersonDto, MoviePerson>();

        }
    }
}
