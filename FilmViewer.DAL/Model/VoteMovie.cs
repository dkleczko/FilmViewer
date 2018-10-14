using System.ComponentModel.DataAnnotations;

namespace FilmViewer.DAL.Model
{
    public class VoteMovie
    {
        [Key]
        public int Id { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int VoteScore { get; set; }
    }
}
