using FluentAssertions;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Repositories;

namespace Restaurant.Tests.Dishes;

public class DishRepositoryTests : TestBase
{
    [Fact]
    public async Task GetByIdAsync_ForExistingDish_ReturnsDish()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new DishRepository(dbContext);
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category"
        };
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();

        var dish = new Dish
        {
            Name = "Test Dish",
            Description = "Test Description",
            Price = 10.99m,
            KiloCalories = 500,
            RestaurantId = restaurant.Id
        };
        await dbContext.Dishes.AddAsync(dish);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(restaurant.Id, dish.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Test Dish");
        result.Description.Should().Be("Test Description");
        result.Price.Should().Be(10.99m);
        result.KiloCalories.Should().Be(500);
        result.RestaurantId.Should().Be(restaurant.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ForNonExistingDish_ReturnsNull()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new DishRepository(dbContext);

        // Act
        var result = await repository.GetByIdAsync(1, 1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllForRestaurantAsync_ReturnsAllDishesForRestaurant()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new DishRepository(dbContext);
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category"
        };
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();

        var dishes = new List<Dish>
        {
            new()
            {
                Name = "Dish 1",
                Description = "Description 1",
                Price = 10.99m,
                KiloCalories = 500,
                RestaurantId = restaurant.Id
            },
            new()
            {
                Name = "Dish 2",
                Description = "Description 2",
                Price = 15.99m,
                KiloCalories = 700,
                RestaurantId = restaurant.Id
            }
        };
        await dbContext.Dishes.AddRangeAsync(dishes);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAllForRestaurantAsync(restaurant.Id);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(d => d.Name == "Dish 1");
        result.Should().Contain(d => d.Name == "Dish 2");
    }

    [Fact]
    public async Task CreateAsync_CreatesNewDish()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new DishRepository(dbContext);
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category"
        };
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();

        var dish = new Dish
        {
            Name = "New Dish",
            Description = "New Description",
            Price = 12.99m,
            KiloCalories = 600,
            RestaurantId = restaurant.Id
        };

        // Act
        var id = await repository.CreateAsync(dish);

        // Assert
        var createdDish = await dbContext.Dishes.FindAsync(id);
        createdDish.Should().NotBeNull();
        createdDish!.Name.Should().Be("New Dish");
        createdDish.Description.Should().Be("New Description");
        createdDish.Price.Should().Be(12.99m);
        createdDish.KiloCalories.Should().Be(600);
        createdDish.RestaurantId.Should().Be(restaurant.Id);
    }

    [Fact]
    public async Task DeleteAsync_DeletesExistingDish()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new DishRepository(dbContext);
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category"
        };
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();

        var dish = new Dish
        {
            Name = "Dish to Delete",
            Description = "Description",
            Price = 9.99m,
            RestaurantId = restaurant.Id
        };
        await dbContext.Dishes.AddAsync(dish);
        await dbContext.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(restaurant.Id, dish.Id);

        // Assert
        var deletedDish = await dbContext.Dishes.FindAsync(dish.Id);
        deletedDish.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingDish()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var repository = new DishRepository(dbContext);
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category"
        };
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();

        var dish = new Dish
        {
            Name = "Original Dish",
            Description = "Original Description",
            Price = 9.99m,
            KiloCalories = 400,
            RestaurantId = restaurant.Id
        };
        await dbContext.Dishes.AddAsync(dish);
        await dbContext.SaveChangesAsync();

        // Modify the dish
        dish.Name = "Updated Dish";
        dish.Description = "Updated Description";
        dish.Price = 14.99m;
        dish.KiloCalories = 600;

        // Act
        await repository.UpdateAsync(dish);

        // Assert
        var updatedDish = await dbContext.Dishes.FindAsync(dish.Id);
        updatedDish.Should().NotBeNull();
        updatedDish!.Name.Should().Be("Updated Dish");
        updatedDish.Description.Should().Be("Updated Description");
        updatedDish.Price.Should().Be(14.99m);
        updatedDish.KiloCalories.Should().Be(600);
    }
} 