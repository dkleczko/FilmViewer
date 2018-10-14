using System.ComponentModel.DataAnnotations;

namespace FilmViewer.Models.Moderator
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}