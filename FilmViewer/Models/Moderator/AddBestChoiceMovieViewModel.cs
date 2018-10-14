using System.ComponentModel.DataAnnotations;
using FilmViewer.Business.Dto.Domain.Enum;

namespace FilmViewer.Models.Moderator
{
    public class AddBestChoiceMovieViewModel
    {
        [Required]
        public SliderTypeDto SliderType { get; set; }

        [Required]
        public string SliderTitle { get; set; }

        public string SliderContent { get; set; }

        public static string MovieCollectionKey = "Movie";


    }
}