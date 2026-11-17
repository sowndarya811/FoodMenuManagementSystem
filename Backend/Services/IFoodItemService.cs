
using Food_Menu_Management_System.DTOs;

namespace Food_Menu_Management_System.Services
{
    public interface IFoodItemService
    {
        Task<List<FoodItemResponseDto>> GetAllAsync(
            string? category,
            bool? availableOnly);

        Task<FoodItemResponseDto?> GetByIdAsync(int id);

        Task<FoodItemResponseDto> CreateAsync(
            CreateFoodItemDto dto);

        Task<FoodItemResponseDto?> UpdateAsync(
            int id,
            UpdateFoodItemDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
