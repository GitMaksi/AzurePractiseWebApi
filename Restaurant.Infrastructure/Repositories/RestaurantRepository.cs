using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Infrastructure.Repositories;

public class RestaurantRepository(RestaurantsDbContext dbContext)
    : IRestaurantRepository
{
    public async Task<IEnumerable<Domain.Entities.Restaurant>> AllAsync()
    {
        return await dbContext.Restaurants
            .Include(restaurant => restaurant.Dishes)
            .ToListAsync();
    }

    public async Task<Domain.Entities.Restaurant?> GetById(int id)
    {
        return await dbContext.Restaurants
            .Include(restaurant => restaurant.Dishes)
            .FirstOrDefaultAsync(restaurant => restaurant.Id == id);
    }

    public async Task<int> CreateAsync(Domain.Entities.Restaurant restaurant)
    {
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var resourceToDelete = await dbContext.Restaurants.FirstOrDefaultAsync(restaurant=> restaurant.Id == id);
        dbContext.Restaurants.Remove(resourceToDelete);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    => await dbContext.SaveChangesAsync();
}