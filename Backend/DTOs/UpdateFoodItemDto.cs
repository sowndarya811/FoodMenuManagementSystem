
using System.ComponentModel.DataAnnotations;

namespace Food_Menu_Management_System.DTOs
{
    public class UpdateFoodItemDto
    {
        [Required(ErrorMessage = "Food name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(0.01, 999999.99,
            ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}
