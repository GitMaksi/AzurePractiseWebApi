using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Restaurants;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>>  GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(int id);
}