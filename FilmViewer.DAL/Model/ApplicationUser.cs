using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmViewer.DAL.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        public virtual ICollection<Movie> Favourites { get; set; }
        public virtual ICollection<VoteMovie> VoteMovie { get; set; }
        public virtual ICollection<VoteMoviePerson> VoteMoviePerson { get; set; }
        public ApplicationUser()
        {
            Favourites = new List<Movie>();
            VoteMovie = new List<VoteMovie>();
            VoteMoviePerson = new List<VoteMoviePerson>();
        }
    }
}
