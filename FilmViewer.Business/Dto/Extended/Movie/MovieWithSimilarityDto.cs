using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Movie
{
    public class MovieWithSimilarityDto : MovieDto
    {
        public double Similarity { get; set; }
    }
}
