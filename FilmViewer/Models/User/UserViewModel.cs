using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmViewer.Models.User
{
    public class UserViewModel
    {
        public int LikedMovies { get; set; }
        public int VotedMovies { get; set; }
        public int VotedActors { get; set; }
    }
}