using FilmViewer.Business.Dto.Domain.Enum;

namespace FilmViewer.Business.Dto.Domain
{
    public class MainMoviesDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public SliderTypeDto SliderType { get; set; }
        public string MovieTitle { get; set; }
        public string Title { get; set; }
        public string MovieImagePath { get; set; }
        public string Content { get; set; }
    }
}
