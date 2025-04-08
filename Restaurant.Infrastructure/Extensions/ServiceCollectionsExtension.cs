using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Seeders;

namespace Restaurant.Infrastructure.Extensions;

public static class ServiceCollectionsExtension
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration )
    {
        var connectionString = configuration.GetConnectionString("RestaurantDb");
        services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
    }
}