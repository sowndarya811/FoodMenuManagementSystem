
using Microsoft.AspNetCore.Mvc;
using Food_Menu_Management_System.DTOs;
using Food_Menu_Management_System.Services;

namespace Food_Menu_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodItemService _foodItemService;

        public FoodItemsController(IFoodItemService foodItemService)
        {
            _foodItemService = foodItemService;
        }

        // GET: api/fooditems
        // GET: api/fooditems?category=Snacks
        // GET: api/fooditems?category=Snacks&availableOnly=true
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? category,
            [FromQuery] bool? availableOnly)
        {
            var foodItems = await _foodItemService.GetAllAsync(
                category,
                availableOnly);

            return Ok(foodItems);
        }

        // GET: api/fooditems/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var foodItem = await _foodItemService.GetByIdAsync(id);

            if (foodItem == null)
            {
                return NotFound(new
                {
                    message = $"Food item with ID {id} was not found."
                });
            }

            return Ok(foodItem);
        }

        // POST: api/fooditems
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateFoodItemDto dto)
        {
            var createdFoodItem = await _foodItemService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdFoodItem.Id },
                createdFoodItem);
        }

        // PUT: api/fooditems/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateFoodItemDto dto)
        {
            var updatedFoodItem = await _foodItemService.UpdateAsync(
                id,
                dto);

            if (updatedFoodItem == null)
            {
                return NotFound(new
                {
                    message = $"Food item with ID {id} was not found."
                });
            }

            return Ok(updatedFoodItem);
        }

        // DELETE: api/fooditems/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _foodItemService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound(new
                {
                    message = $"Food item with ID {id} was not found."
                });
            }

            return NoContent();
        }
    }
}
