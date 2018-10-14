using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmViewer.DAL.Model
{
    public class UserActivities
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ActivityDate { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


    }
}
