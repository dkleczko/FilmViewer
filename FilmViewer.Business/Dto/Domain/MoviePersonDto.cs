namespace FilmViewer.Business.Dto.Domain
{
    public class MoviePersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }
        public string PhotoUrl { get; set; }
        public int VoteScores { get; set; }
        public int VoteCount { get; set; }
    }
}
