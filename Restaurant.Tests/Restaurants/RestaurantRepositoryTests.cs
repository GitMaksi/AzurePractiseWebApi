using FluentAssertions;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Repositories;

namespace Restaurant.Tests.Restaurants;

public class RestaurantRepositoryTests : TestBase
{
    [Fact]
    public async Task GetById_ForExistingRestaurant_ReturnsRestaurant()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new RestaurantRepository(dbContext);
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true
        };
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetById(restaurant.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Test Restaurant");
        result.Description.Should().Be("Test Description");
        result.Category.Should().Be("Test Category");
        result.HasDelivery.Should().BeTrue();
    }

    [Fact]
    public async Task GetById_ForNonExistingRestaurant_ReturnsNull()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new RestaurantRepository(dbContext);

        // Act
        var result = await repository.GetById(1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AllAsync_ReturnsAllRestaurants()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new RestaurantRepository(dbContext);
        var restaurants = new List<Domain.Entities.Restaurant>
        {
            new()
            {
                Name = "Restaurant 1",
                Description = "Description 1",
                Category = "Category 1",
                HasDelivery = true
            },
            new()
            {
                Name = "Restaurant 2",
                Description = "Description 2",
                Category = "Category 2",
                HasDelivery = false
            }
        };
        await dbContext.Restaurants.AddRangeAsync(restaurants);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.AllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(r => r.Name == "Restaurant 1");
        result.Should().Contain(r => r.Name == "Restaurant 2");
    }

    [Fact]
    public async Task CreateAsync_CreatesNewRestaurant()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new RestaurantRepository(dbContext);
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = "New Restaurant",
            Description = "New Description",
            Category = "New Category",
            HasDelivery = true
        };

        // Act
        var id = await repository.CreateAsync(restaurant);

        // Assert
        var createdRestaurant = await dbContext.Restaurants.FindAsync(id);
        createdRestaurant.Should().NotBeNull();
        createdRestaurant!.Name.Should().Be("New Restaurant");
        createdRestaurant.Description.Should().Be("New Description");
        createdRestaurant.Category.Should().Be("New Category");
        createdRestaurant.HasDelivery.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_DeletesExistingRestaurant()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new RestaurantRepository(dbContext);
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = "Restaurant to Delete",
            Description = "Description",
            Category = "Category",
            HasDelivery = true
        };
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(restaurant.Id);

        // Assert
        var deletedRestaurant = await dbContext.Restaurants.FindAsync(restaurant.Id);
        deletedRestaurant.Should().BeNull();
    }
} 