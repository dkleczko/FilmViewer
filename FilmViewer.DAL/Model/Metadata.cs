using System.ComponentModel.DataAnnotations;

namespace FilmViewer.DAL.Model
{
    public class Metadata
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
