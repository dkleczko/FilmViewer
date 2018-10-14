using System;
using System.ComponentModel.DataAnnotations;

namespace FilmViewer.DAL.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }


        public Movie Movie { get; set; }
    }
}
