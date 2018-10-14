using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Movie
{
    public class MovieDetailsExtendedDto : MovieDetailsDto
    {
        public bool IsUserFavourite { get; set; }
        public int CurrentUserVote { get; set; }
        public bool HasUserVoteActor { get; set; }

        public List<CategoryDto> Categories { get; set; }
        public string DirectorName { get; set; }
        public int DirectorId { get; set; }

        public List<PhotoPathDto> Photos { get; set; }
        public List<ActorDto> Actors { get; set; }

        public int CommentsCount { get; set; }
    }
}
