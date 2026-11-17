
using Food_Menu_Management_System.DTOs;

namespace Food_Menu_Management_System.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllAsync();

        Task<CategoryResponseDto?> GetByIdAsync(int id);

        Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);

        Task<CategoryResponseDto?> UpdateAsync(int id, UpdateCategoryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
