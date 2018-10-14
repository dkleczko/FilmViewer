using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Actor
{
    public class VotedActorsByUserDto
    {
        public ActorDto Actor { get; set; }
        public int CurrentVoteScore { get; set; }
    }
}
