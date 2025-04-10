using MediatR;

namespace Restaurant.Application.Restaurants.Commands.CreateRestaurant;

public record CreateRestaurantCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public bool HasDelivery { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
}