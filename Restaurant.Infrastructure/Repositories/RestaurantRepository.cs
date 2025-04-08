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
}