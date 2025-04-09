using MediatR;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants;

public record GetAllRestaurantQuery : IRequest<IEnumerable<RestaurantDto>>
{
}