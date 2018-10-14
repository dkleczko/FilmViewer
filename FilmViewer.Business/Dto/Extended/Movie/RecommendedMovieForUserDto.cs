using System;
using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Movie
{
    public class RecommendedMovieForUserDto : MovieDto
    {
        public int Duration { get; set; }
        public DateTime? PremiereDate { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
