using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Infrastructure.Repositories;

public class DishRepository : IDishRepository
{
    private readonly RestaurantsDbContext _dbContext;

    public DishRepository(RestaurantsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Dish>> GetAllForRestaurantAsync(int restaurantId)
    {
        return await _dbContext.Dishes
            .Where(d => d.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<Dish?> GetByIdAsync(int restaurantId, int dishId)
    {
        return await _dbContext.Dishes
            .FirstOrDefaultAsync(d => d.RestaurantId == restaurantId && d.Id == dishId);
    }

    public async Task<int> CreateAsync(Dish dish)
    {
        await _dbContext.Dishes.AddAsync(dish);
        await _dbContext.SaveChangesAsync();
        return dish.Id;
    }

    public async Task DeleteAsync(int restaurantId, int dishId)
    {
        var dish = await GetByIdAsync(restaurantId, dishId);
        if (dish != null)
        {
            _dbContext.Dishes.Remove(dish);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Dish dish)
    {
        _dbContext.Dishes.Update(dish);
        await _dbContext.SaveChangesAsync();
    }
} 