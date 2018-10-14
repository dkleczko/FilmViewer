using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Movie
{
    public class VotedMoviesByUserDto
    {
        public MovieDto Movie { get; set; }
        public int CurrentVoteScore { get; set; }
    }
}
