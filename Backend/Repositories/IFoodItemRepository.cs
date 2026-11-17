
using Food_Menu_Management_System.Models;

namespace Food_Menu_Management_System.Repositories
{
    public interface IFoodItemRepository
    {
        Task<List<FoodItem>> GetAllAsync(
            string? category,
            bool? availableOnly);

        Task<FoodItem?> GetByIdAsync(int id);

        Task<FoodItem> AddAsync(FoodItem foodItem);

        Task<FoodItem?> UpdateAsync(
            int id,
            FoodItem foodItem);

        Task<bool> DeleteAsync(int id);
    }
}
