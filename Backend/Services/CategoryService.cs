
using Food_Menu_Management_System.DTOs;
using Food_Menu_Management_System.Models;
using Food_Menu_Management_System.Repositories;

namespace Food_Menu_Management_System.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(c => MapToDto(c)).ToList();
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            return category == null ? null : MapToDto(category);
        }

        public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
        {
            string name = dto.Name.Trim();

            var existing = await _repository.GetByNameAsync(name);

            if (existing != null)
            {
                throw new InvalidOperationException(
                    "A category with this name already exists.");
            }

            var category = new Category
            {
                Name = name,
                Description = dto.Description?.Trim(),
                IsActive = true
            };

            await _repository.AddAsync(category);

            return MapToDto(category);
        }

        public async Task<CategoryResponseDto?> UpdateAsync(
            int id, UpdateCategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return null;
            }

            string newName = dto.Name.Trim();

            var existing = await _repository.GetByNameAsync(newName);

            if (existing != null && existing.Id != id)
            {
                throw new InvalidOperationException(
                    "Another category with this name already exists.");
            }

            // If the name changes, prevent breaking existing food items.
            if (!string.Equals(category.Name, newName,
                StringComparison.OrdinalIgnoreCase))
            {
                bool isUsed = await _repository.IsUsedByFoodItemsAsync(
                    category.Name);

                if (isUsed)
                {
                    throw new InvalidOperationException(
                        "This category is used by food items. " +
                        "You cannot rename it until those food items are updated.");
                }
            }

            category.Name = newName;
            category.Description = dto.Description?.Trim();
            category.IsActive = dto.IsActive;

            await _repository.UpdateAsync(category);

            return MapToDto(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return false;
            }

            bool isUsed = await _repository.IsUsedByFoodItemsAsync(
                category.Name);

            if (isUsed)
            {
                throw new InvalidOperationException(
                    "Cannot delete this category because food items are using it.");
            }

            await _repository.DeleteAsync(category);

            return true;
        }

        private static CategoryResponseDto MapToDto(Category category)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }
    }
}
