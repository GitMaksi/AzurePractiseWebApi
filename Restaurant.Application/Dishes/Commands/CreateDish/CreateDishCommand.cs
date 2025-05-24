using MediatR;

namespace Restaurant.Application.Dishes.Commands.CreateDish;

public record CreateDishCommand : IRequest<int>
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public int? KiloCalories { get; init; }
    public int RestaurantId { get; set; }
}