using System.ComponentModel.DataAnnotations;

namespace FilmViewer.Models.Moderator
{
    public class EditCategoryViewModel
    {
        [Required(ErrorMessage = "Cannot edit current Category because there is no id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}