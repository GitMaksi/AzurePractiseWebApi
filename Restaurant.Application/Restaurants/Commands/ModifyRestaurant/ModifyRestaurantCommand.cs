using System.Text.Json.Serialization;
using MediatR;

namespace Restaurant.Application.Restaurants.Commands.ModifyRestaurant;

public record ModifyRestaurantCommand : IRequest<bool>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public bool HasDelivery { get; init; }
}