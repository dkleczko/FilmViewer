using System.ComponentModel.DataAnnotations;

namespace FilmViewer.DAL.Model
{
    public class VoteMoviePerson
    {
        [Key]
        public int Id { get; set; }
        public virtual MoviePerson MoviePerson { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int VoteScore { get; set; }
    }
}
