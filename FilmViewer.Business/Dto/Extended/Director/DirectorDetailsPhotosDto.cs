using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Director
{
    public class DirectorDetailsPhotosDto :DirectorDetailsDto
    {
        public List<PhotoPathDto> Photos { get; set; }

    }
}
