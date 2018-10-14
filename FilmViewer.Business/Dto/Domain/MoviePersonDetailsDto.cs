using System;

namespace FilmViewer.Business.Dto.Domain
{
    public class MoviePersonDetailsDto : MoviePersonDto
    {
        public DateTime? Birthdate { get; set; }
        public string BirthPlace { get; set; }
        public string Biography { get; set; }
        public string MaritalStatus { get; set; }
    }
}
