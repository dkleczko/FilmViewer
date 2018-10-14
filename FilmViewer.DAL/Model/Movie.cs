using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmViewer.DAL.Model
{
    public class Movie
    {
        [Key]
        [Display(Name = "Numer Filmu")]
        public int Id { get; set; }

        [MaxLength(400)]
        [Required]
        [Display(Name = "Tytuł Pl")]
        public string TitlePl { get; set; }

        public string Folder { get; set; }

        [MaxLength(400)]
        public string TitleEng { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PremiereDate { get; set; }

        public string ProductionCountries { get; set; }

        public int Duration { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PhotoPath { get; set; }

        public string HeraldUrl { get; set; }

        public virtual ICollection<PhotoPath> PhotoUrls { get; set; }

        public int VoteScores { get; set; }

        public int VoteCount { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
        [NotMapped]
        public double similarity { get; set; }

        public virtual ICollection<VoteMovie> VoteMovie { get; set; }


        [DataType(DataType.Text)]
        public string Metadata { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual Director Director { get; set; }

        public virtual ICollection<ApplicationUser> Favourites { get; set; }
    }
}
