using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class DirectorDetailsDtoProfile : Profile
    {
        public DirectorDetailsDtoProfile()
        {
            CreateMap<Director, DirectorDetailsDto>()
                .IncludeBase<MoviePerson, MoviePersonDetailsDto>();

            CreateMap<DirectorDetailsDto, Director>()
                .IncludeBase<MoviePersonDetailsDto, MoviePerson>();
        }
    }
}
