using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Models.Moderator
{
    public class MovieEditViewModel
    {
        [Required(ErrorMessage = "MovieId is required.")]
        public int MovieId { get; set; }

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


        public string Folder { get; set; }

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


        public List<CategoryDto> Categories { get; set; }

        public List<ActorDto> Actors { get; set; }

        public DirectorDto Director { get; set; }

        public List<PhotoPathDto> Photos { get; set; }
    }
}