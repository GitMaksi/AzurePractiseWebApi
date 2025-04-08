using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants;

public class RestaurantsService(IRestaurantRepository restaurantRepository,
    ILogger<RestaurantsService> logger, IMapper  mapper)
    : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Get all restaurants function started");
        var restaurants = await restaurantRepository.AllAsync();
        var allRestaurants = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        logger.LogInformation($"Get all restaurants function finished, retrieved {allRestaurants.Count()} restaurants");
        return allRestaurants;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation($"Get restaurant function started, id: {id}");
        var restaurant = await restaurantRepository.GetById(id);
        logger.LogInformation($"Get restaurant function finished, id: {id}");
        return mapper.Map<RestaurantDto?>(restaurant);
    }
}