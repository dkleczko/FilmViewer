using System;

namespace FilmViewer.Business.Dto.Domain
{
    public class MovieDetailsDto : MovieDto
    {
        public string Content { get; set; }
        public DateTime? PremiereDate { get; set; }
        public string ProductionCountries { get; set; }
        public int Duration { get; set; }
        public string HearldUrl { get; set; }
    }
}
