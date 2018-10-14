using System.ComponentModel.DataAnnotations;
using FilmViewer.DAL.Model.Enum;

namespace FilmViewer.DAL.Model
{
    public class MainMovie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual Movie Movie { get; set; }
        [Required]
        public string MovieImagePath { get; set; }
        [Required]
        public SliderType SliderType { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
