namespace FilmViewer.Business.Dto.Domain
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Folder { get; set; }
        public string PhotoUrl { get; set; }
        public int VoteScores { get; set; }
        public int VoteCount { get; set; }
    }
}
