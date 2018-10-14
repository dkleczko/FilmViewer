using System;

namespace FilmViewer.Business.Dto.Extended.Actor
{
    public class CurrentActorVoteDto
    {
        public int Score { get; set; }
        public int VoteCount { get; set; }

        public string VoteResult
        {
            get { return VoteCount != 0 ? Math.Round((decimal) Score / (decimal) VoteCount, 1).ToString() : "0,0"; }
        }
    }
}
