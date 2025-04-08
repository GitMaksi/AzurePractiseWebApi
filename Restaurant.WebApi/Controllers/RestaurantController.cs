using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants;

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
}