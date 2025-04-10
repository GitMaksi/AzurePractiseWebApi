using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants.Commands.CreateRestaurant;
using Restaurant.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurant.Application.Restaurants.Commands.ModifyRestaurant;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurant.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantController(ILogger<RestaurantController> logger, IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantQuery());
        return Ok(restaurants);
    }

    [HttpGet("{restaurantId}")]
    public async Task<IActionResult> GetById([FromRoute] int restaurantId)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(restaurantId));
        
        if (restaurant is null)
        {
            logger.LogError($"Restaurant not found, id: {restaurantId}");
            return NotFound();
        }
        
        logger.LogInformation($"Get restaurant function returned id: {restaurantId}");
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
    {
        var restaurantId = await  mediator.Send(createRestaurantCommand);
        return CreatedAtAction(nameof(GetById), new { restaurantId }, null);
    }

    [HttpDelete("{restaurantId}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int restaurantId)
    {
        logger.LogInformation($"Delete resource function called on resource id: {restaurantId}");
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand(restaurantId));
        
        if (isDeleted)
            return NoContent();
        
        return NotFound();
    }

    [HttpPatch("{restaurantId}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute]int restaurantId, [FromBody] ModifyRestaurantCommand request)
    {
        request.Id = restaurantId;
        var modificationResult = await mediator.Send(request);
        
        if (modificationResult)
            return NoContent();
        
        return NotFound();
    }
}