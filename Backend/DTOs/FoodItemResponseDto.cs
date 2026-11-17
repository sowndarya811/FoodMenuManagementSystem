
namespace Food_Menu_Management_System.DTOs
{
    public class FoodItemResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}
