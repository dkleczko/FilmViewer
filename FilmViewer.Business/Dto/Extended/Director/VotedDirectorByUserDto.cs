using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Director
{
    public class VotedDirectorByUserDto
    {
        public DirectorDto Director { get; set; }
        public int CurrentUserVoteScore { get; set; }
    }
}
