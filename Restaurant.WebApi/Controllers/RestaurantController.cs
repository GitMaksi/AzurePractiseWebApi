using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantController(IRestaurantsService restaurantsService, ILogger<RestaurantController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await restaurantsService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{restaurantId}")]
    public async Task<IActionResult> GetById([FromRoute] int restaurantId)
    {
        var restaurant = await restaurantsService.GetRestaurantById(restaurantId);
        
        if (restaurant is null)
        {
            logger.LogError($"Restaurant not found, id: {restaurantId}");
            return NotFound();
        }
        
        logger.LogInformation($"Get restaurant function returned id: {restaurantId}");
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        var restaurantId = await restaurantsService.CreateRestaurantAsync(createRestaurantDto);
        return CreatedAtAction(nameof(GetById), new { restaurantId }, null);
    }

    [HttpDelete("{restaurantId}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int restaurantId)
    {
        logger.LogInformation($"Delete resource function called on resource id: {restaurantId}");
        await restaurantsService.DeleteRestaurantAsync(restaurantId);
        return Ok();
    }
}