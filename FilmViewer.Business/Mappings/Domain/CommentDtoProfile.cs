using AutoMapper;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Mappings.Domain
{
    internal class CommentDtoProfile : Profile
    {
        public CommentDtoProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(p => p.UserId, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(p => p.CommentId, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.Date, opt => opt.MapFrom(x => x.DateTime))
                .ForMember(p => p.Text, opt => opt.MapFrom(x => x.Content))
                .ForMember(p => p.UserName, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(p => p.MovieId, opt => opt.MapFrom(x => x.Movie.Id));
        }
    }
}
