using System.ComponentModel.DataAnnotations;

namespace FilmViewer.Models.Moderator
{
    public class EditMetadataViewModel
    {
        [Required(ErrorMessage = "There is no metadata id.")]
        public int MetadataId { get; set; }

        [Required(ErrorMessage = "Metadata name is required.")]
        public string MetadataName { get; set; }
    }
}