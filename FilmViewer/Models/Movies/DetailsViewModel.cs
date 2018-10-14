using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Movie;

namespace FilmViewer.Models.Movies
{
    public class DetailsViewModel
    {
        public MovieDetailsExtendedDto Movie { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}