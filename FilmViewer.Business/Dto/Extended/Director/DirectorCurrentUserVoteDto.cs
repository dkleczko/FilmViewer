using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Director
{
    public class DirectorCurrentUserVoteDto : DirectorDetailsDto
    {
        public int CurrentUserVote { get; set; }
        public bool HasUserVoteDirector { get; set; }
    }
}
