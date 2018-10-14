using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class ActorDetailsDtoProfile : Profile
    {
        public ActorDetailsDtoProfile()
        {
            CreateMap<Actor, ActorDetailsDto>()
                .IncludeBase<MoviePerson, MoviePersonDetailsDto>();

            CreateMap<ActorDetailsDto, Actor>()
                .IncludeBase<MoviePersonDetailsDto, MoviePerson>();

        }
    }
}
