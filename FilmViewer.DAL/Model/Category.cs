using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmViewer.DAL.Model
{
    public class Category
    {
        public Category()
        {
            Movies = new List<Movie>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
