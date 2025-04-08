using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence;

public class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : DbContext(options)
{
    internal DbSet<Domain.Entities.Restaurant> Restaurants { get; set; } 
    internal DbSet<Dish> Dishes { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Domain.Entities.Restaurant>()
            .OwnsOne(restaurant => restaurant.Address);
        
        modelBuilder.Entity<Domain.Entities.Restaurant>()
            .HasMany(restaurant => restaurant.Dishes)
            .WithOne()
            .HasForeignKey(dish => dish.RestaurantId); 
    }
}