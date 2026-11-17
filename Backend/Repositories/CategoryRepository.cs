
using Microsoft.EntityFrameworkCore;
using Food_Menu_Management_System.Data;
using Food_Menu_Management_System.Models;

namespace Food_Menu_Management_System.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            string normalizedName = name.Trim().ToLower();

            return await _context.Categories
                .FirstOrDefaultAsync(c =>
                    c.Name.ToLower() == normalizedName);
        }

        public async Task<bool> IsUsedByFoodItemsAsync(string categoryName)
        {
            return await _context.FoodItems
                .AnyAsync(f => f.Category == categoryName);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
