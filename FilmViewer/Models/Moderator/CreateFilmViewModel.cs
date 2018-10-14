using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FilmViewer.Models.Moderator
{
    public class CreateFilmViewModel
    {
        [Required(ErrorMessage = "Title field is required.")]
        [Display(Name = "Title")]

        public string Title { get; set; }
        [Display(Name = "Premier Date")]

        public DateTime? PremiereDate { get; set; }

        [Display(Name = "Duration")]
        [Range(0, int.MaxValue, ErrorMessage = "Duration should be positive value.")]
        public int Duration { get; set; }

        [Display(Name = "Production Countries")]
        public string ProductionCountries { get; set; }

        [Display(Name = "Trailer")]
        public string HeraldUrl { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }


        public string PremiereDateString
        {
            get
            {
                if (PremiereDate.HasValue)
                {
                    return PremiereDate.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }

        public static string DirectorCollectionKey = "Director";

        public static string ActorCollectionKey = "Actor";

        public static string MetadataCollectionKey = "MetadataId";

        public static string CategoryCollectionKey = "Category";
    }
}