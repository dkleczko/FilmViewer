using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Models.Moderator
{
    public class ActorEditViewModel
    {
        public int ActorId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "FullName")]
        public string Name { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Birthdate")]
        public DateTime? Birthdate { get; set; }
        [Display(Name = "BirthPlace")]
        public string BirthPlace { get; set; }
        [Display(Name = "Biography")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        public string Folder { get; set; }

        public string BirthdateString
        {
            get
            {
                if (Birthdate.HasValue)
                {
                    return Birthdate.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }

        public List<PhotoPathDto> Photos { get; set; }
    }
}