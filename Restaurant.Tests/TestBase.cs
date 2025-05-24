using Microsoft.EntityFrameworkCore;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Tests;

public abstract class TestBase
{
    protected RestaurantsDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<RestaurantsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new RestaurantsDbContext(options);
    }
} 