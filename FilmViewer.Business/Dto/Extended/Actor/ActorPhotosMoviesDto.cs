using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Actor
{
    public class ActorPhotosMoviesDto : ActorDetailsDto
    {
        public List<PhotoPathDto> Photos { get; set; }
        public List<MovieDto> Movies { get; set; }

        public int CurrentUserVote { get; set; }
        public bool HasUserVoteActor { get; set; }
    }
}
