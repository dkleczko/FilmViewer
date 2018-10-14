using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmViewer.DAL.Model
{
    public class MoviePerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [MaxLength(200)]
        public string BirthPlace { get; set; }
        public string Folder { get; set; }

        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }

        public virtual ICollection<PhotoPath> PhotoUrls { get; set; }
        public string MaritalStatus { get; set; }

        public int VoteScores { get; set; }

        public int VoteCount { get; set; }

        [NotMapped]
        public double Similarity { get; set; }

        public virtual ICollection<Movie> ConnectedMovies { get; set; }

        public virtual ICollection<VoteMoviePerson> Votes { get; set; }

        public bool Equals(MoviePerson other)
        {
            // Would still want to check for null etc. first.
            return this.Id == other.Id;
        }

    }
}
