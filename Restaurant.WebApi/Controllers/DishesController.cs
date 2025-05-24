using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.Commands.DeleteDish;
using Restaurant.Application.Dishes.Commands.UpdateDish;
using Restaurant.Application.Dishes.Queries.GetAllDishes;
using Restaurant.Application.Dishes.Queries.GetDishById;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
public class DishesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DishesController> _logger;

    public DishesController(IMediator mediator, ILogger<DishesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAll([FromRoute] int restaurantId)
    {
        var dishes = await _mediator.Send(new GetAllDishesQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetById([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var dish = await _mediator.Send(new GetDishByIdQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var dishId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { restaurantId, dishId }, null);
    }

    [HttpDelete("{dishId}")]
    public async Task<ActionResult> DeleteDish([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        await _mediator.Send(new DeleteDishCommand(restaurantId, dishId));
        return NoContent();
    }

    [HttpPut("{dishId}")]
    public async Task<ActionResult> UpdateDish([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] UpdateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        command.DishId = dishId;
        await _mediator.Send(command);
        return NoContent();
    }
}