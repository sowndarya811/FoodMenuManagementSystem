
using System.ComponentModel.DataAnnotations;

namespace Food_Menu_Management_System.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }
    }
}
