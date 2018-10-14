using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Models.Director
{
    public class RatingListViewModel
    {
        public List<DirectorDto> Top100Directors { get; set; }
    }
}