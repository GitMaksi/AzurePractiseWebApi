using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurants;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantsService, RestaurantsService>();

        services.AddAutoMapper(typeof(ServiceCollectionExtenstion).Assembly);
    }
}