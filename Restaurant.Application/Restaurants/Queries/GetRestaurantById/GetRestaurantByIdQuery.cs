using MediatR;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Restaurants.Queries.GetRestaurantById;

public record GetRestaurantByIdQuery(int restaurantId) : IRequest<RestaurantDto?>
{
    public int Id { get; } = restaurantId;
}