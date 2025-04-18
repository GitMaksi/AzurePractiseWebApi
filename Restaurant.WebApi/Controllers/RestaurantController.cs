using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants.Commands.CreateRestaurant;
using Restaurant.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurant.Application.Restaurants.Commands.ModifyRestaurant;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurant.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantController(ILogger<RestaurantController> logger, IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantQuery());
        return Ok(restaurants);
    }

    [HttpGet("{restaurantId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RestaurantDto>> GetById([FromRoute] int restaurantId)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(restaurantId));
        
        logger.LogInformation($"Get restaurant function returned id: {restaurantId}");
        return Ok(restaurant);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
    {
        var restaurantId = await  mediator.Send(createRestaurantCommand);
        return CreatedAtAction(nameof(GetById), new { restaurantId }, null);
    }

    [HttpDelete("{restaurantId}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int restaurantId)
    {
        logger.LogInformation($"Delete resource function called on resource id: {restaurantId}");
        await mediator.Send(new DeleteRestaurantCommand(restaurantId));

        return NoContent();
    }

    [HttpPatch("{restaurantId}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute]int restaurantId, [FromBody] ModifyRestaurantCommand request)
    {
        request.Id = restaurantId;
        await mediator.Send(request);
        return NoContent();
    }
}