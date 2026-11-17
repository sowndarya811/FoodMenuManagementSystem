
using Microsoft.EntityFrameworkCore;
using Food_Menu_Management_System.Data;
using Food_Menu_Management_System.Models;

namespace Food_Menu_Management_System.Repositories
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly ApplicationDbContext _context;

        public FoodItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FoodItem>> GetAllAsync(
            string? category,
            bool? availableOnly)
        {
            IQueryable<FoodItem> query = _context.FoodItems;

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(f =>
                    f.Category.ToLower() == category.ToLower());
            }

            if (availableOnly == true)
            {
                query = query.Where(f => f.IsAvailable);
            }

            return await query.ToListAsync();
        }

        public async Task<FoodItem?> GetByIdAsync(int id)
        {
            return await _context.FoodItems
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<FoodItem> AddAsync(FoodItem foodItem)
        {
            await _context.FoodItems.AddAsync(foodItem);

            await _context.SaveChangesAsync();

            return foodItem;
        }

        public async Task<FoodItem?> UpdateAsync(
            int id,
            FoodItem foodItem)
        {
            var existingFoodItem = await _context.FoodItems
                .FirstOrDefaultAsync(f => f.Id == id);

            if (existingFoodItem == null)
            {
                return null;
            }

            existingFoodItem.Name = foodItem.Name;
            existingFoodItem.Category = foodItem.Category;
            existingFoodItem.Description = foodItem.Description;
            existingFoodItem.Price = foodItem.Price;
            existingFoodItem.IsAvailable = foodItem.IsAvailable;

            await _context.SaveChangesAsync();

            return existingFoodItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var foodItem = await _context.FoodItems
                .FirstOrDefaultAsync(f => f.Id == id);

            if (foodItem == null)
            {
                return false;
            }

            _context.FoodItems.Remove(foodItem);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
