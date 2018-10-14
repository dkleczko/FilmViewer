using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Director
{
    public class DirectorPhotosMoviesDto : DirectorDetailsPhotosDto
    {
        public List<MovieDto> Movies { get; set; }

        public int CurrentUserVote { get; set; }
        public bool HasUserVoteDirector { get; set; }
    }
}
