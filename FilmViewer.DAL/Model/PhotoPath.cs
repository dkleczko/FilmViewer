using System.ComponentModel.DataAnnotations;

namespace FilmViewer.DAL.Model
{
    public class PhotoPath
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
    }
}
