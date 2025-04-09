using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantQueryHandler(ILogger<GetAllRestaurantQueryHandler> logger,  IMapper mapper, IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all restaurants function started");
        var restaurants = await restaurantRepository.AllAsync();
        var allRestaurants = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        logger.LogInformation($"Get all restaurants function finished, retrieved {allRestaurants.Count()} restaurants");
        return allRestaurants;
    }
}