using System.ComponentModel.DataAnnotations;

namespace FilmViewer.Models.Moderator
{
    public class CreateMetadataViewModel
    {
        [Required(ErrorMessage = "Metadata name is required.")]
        public string Name { get; set; }
    }
}