
using Food_Menu_Management_System.Models;

namespace Food_Menu_Management_System.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task<Category?> GetByNameAsync(string name);

        Task<bool> IsUsedByFoodItemsAsync(string categoryName);

        Task AddAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(Category category);
    }
}
