using System.Collections.Generic;
using FilmViewer.Business.Dto.Extended.Movie;

namespace FilmViewer.Models.User
{
    public class RecommendationsViewModel
    {
        public List<MovieWithSimilarityDto> Movies { get; set; }
    }
}