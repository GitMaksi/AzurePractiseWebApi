namespace Restaurant.Domain.Repositories;

public interface IDishRepository
{
    Task<IEnumerable<Entities.Dish>> GetAllForRestaurantAsync(int restaurantId);
    Task<Entities.Dish?> GetByIdAsync(int restaurantId, int dishId);
    Task<int> CreateAsync(Entities.Dish dish);
    Task DeleteAsync(int restaurantId, int dishId);
    Task UpdateAsync(Entities.Dish dish);
} 