using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "Photo")]
        public IFormFile? FormFile { get; set; }
    }
}
