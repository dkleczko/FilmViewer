using System.Collections.Generic;
using FilmViewer.Business.Dto.Extended.Movie;

namespace FilmViewer.Models.Home
{
    public class SelectedMoviesForCurrentUserViewModel
    {
        public List<RecommendedMovieForUserDto> PrepardMoviesForUser { get; set; }

    }
}