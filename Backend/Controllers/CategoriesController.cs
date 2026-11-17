
using Microsoft.AspNetCore.Mvc;
using Food_Menu_Management_System.DTOs;
using Food_Menu_Management_System.Services;

namespace Food_Menu_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();

            return Ok(categories);
        }

        // GET: api/categories/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Category not found."
                });
            }

            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            try
            {
                var category = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = category.Id },
                    category);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // PUT: api/categories/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, UpdateCategoryDto dto)
        {
            try
            {
                var category = await _service.UpdateAsync(id, dto);

                if (category == null)
                {
                    return NotFound(new
                    {
                        message = "Category not found."
                    });
                }

                return Ok(category);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // DELETE: api/categories/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleted = await _service.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        message = "Category not found."
                    });
                }

                return Ok(new
                {
                    message = "Category deleted successfully."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
