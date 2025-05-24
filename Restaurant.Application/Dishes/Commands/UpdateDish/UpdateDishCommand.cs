using System.Text.Json.Serialization;
using MediatR;

namespace Restaurant.Application.Dishes.Commands.UpdateDish;

public record UpdateDishCommand : IRequest
{
    [JsonIgnore]
    public int RestaurantId { get; set; }
    
    [JsonIgnore]
    public int DishId { get; set; }
    
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public int? KiloCalories { get; init; }
} 