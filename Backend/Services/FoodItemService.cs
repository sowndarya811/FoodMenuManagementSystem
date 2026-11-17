
using Food_Menu_Management_System.DTOs;
using Food_Menu_Management_System.Models;
using Food_Menu_Management_System.Repositories;

namespace Food_Menu_Management_System.Services
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IFoodItemRepository _repository;

        public FoodItemService(IFoodItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FoodItemResponseDto>> GetAllAsync(
            string? category,
            bool? availableOnly)
        {
            var foodItems = await _repository.GetAllAsync(
                category,
                availableOnly);

            return foodItems.Select(f => new FoodItemResponseDto
            {
                Id = f.Id,
                Name = f.Name,
                Category = f.Category,
                Description = f.Description,
                Price = f.Price,
                IsAvailable = f.IsAvailable
            }).ToList();
        }

        public async Task<FoodItemResponseDto?> GetByIdAsync(int id)
        {
            var foodItem = await _repository.GetByIdAsync(id);

            if (foodItem == null)
            {
                return null;
            }

            return new FoodItemResponseDto
            {
                Id = foodItem.Id,
                Name = foodItem.Name,
                Category = foodItem.Category,
                Description = foodItem.Description,
                Price = foodItem.Price,
                IsAvailable = foodItem.IsAvailable
            };
        }

        public async Task<FoodItemResponseDto> CreateAsync(
            CreateFoodItemDto dto)
        {
            var foodItem = new FoodItem
            {
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable
            };

            var createdFoodItem = await _repository.AddAsync(foodItem);

            return new FoodItemResponseDto
            {
                Id = createdFoodItem.Id,
                Name = createdFoodItem.Name,
                Category = createdFoodItem.Category,
                Description = createdFoodItem.Description,
                Price = createdFoodItem.Price,
                IsAvailable = createdFoodItem.IsAvailable
            };
        }

        public async Task<FoodItemResponseDto?> UpdateAsync(
            int id,
            UpdateFoodItemDto dto)
        {
            var foodItem = new FoodItem
            {
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable
            };

            var updatedFoodItem = await _repository.UpdateAsync(id, foodItem);

            if (updatedFoodItem == null)
            {
                return null;
            }

            return new FoodItemResponseDto
            {
                Id = updatedFoodItem.Id,
                Name = updatedFoodItem.Name,
                Category = updatedFoodItem.Category,
                Description = updatedFoodItem.Description,
                Price = updatedFoodItem.Price,
                IsAvailable = updatedFoodItem.IsAvailable
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
