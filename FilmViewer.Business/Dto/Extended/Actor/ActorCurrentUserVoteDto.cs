using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Actor
{
    public class ActorCurrentUserVoteDto : ActorDetailsDto
    {
        public int CurrentUserVote { get; set; }
        public bool HasUserVoteActor { get; set; }
    }
}
