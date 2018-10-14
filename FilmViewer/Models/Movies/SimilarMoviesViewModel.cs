using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Models.Movies
{
    public class SimilarMoviesViewModel
    {
        public List<MovieDto> SimilarMovies { get; set; }
    }
}