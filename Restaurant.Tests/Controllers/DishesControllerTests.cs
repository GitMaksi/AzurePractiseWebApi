using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Application.Dishes;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.Commands.DeleteDish;
using Restaurant.Application.Dishes.Commands.UpdateDish;
using Restaurant.Application.Dishes.Queries.GetAllDishes;
using Restaurant.Application.Dishes.Queries.GetDishById;
using Restaurant.WebApi.Controllers;

namespace Restaurant.Tests.Controllers;

public class DishesControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<DishesController>> _loggerMock;
    private readonly DishesController _controller;

    public DishesControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<DishesController>>();
        _controller = new DishesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResultWithDishes()
    {
        // Arrange
        var restaurantId = 1;
        var dishes = new List<DishDto>
        {
            new() { Id = 1, Name = "Dish 1" },
            new() { Id = 2, Name = "Dish 2" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllDishesQuery>(), default))
            .ReturnsAsync(dishes);

        // Act
        var result = await _controller.GetAll(restaurantId);

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedDishes = okResult.Value.Should().BeAssignableTo<IEnumerable<DishDto>>().Subject;
        returnedDishes.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetById_ReturnsOkResultWithDish()
    {
        // Arrange
        var restaurantId = 1;
        var dishId = 1;
        var dish = new DishDto { Id = dishId, Name = "Test Dish" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetDishByIdQuery>(), default))
            .ReturnsAsync(dish);

        // Act
        var result = await _controller.GetById(restaurantId, dishId);

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedDish = okResult.Value.Should().BeOfType<DishDto>().Subject;
        returnedDish.Id.Should().Be(dishId);
        returnedDish.Name.Should().Be("Test Dish");
    }

    [Fact]
    public async Task CreateDish_ReturnsCreatedAtAction()
    {
        // Arrange
        var restaurantId = 1;
        var command = new CreateDishCommand
        {
            Name = "New Dish",
            Description = "Description",
            Price = 9.99m
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateDishCommand>(), default))
            .ReturnsAsync(1);

        // Act
        var result = await _controller.CreateDish(restaurantId, command);

        // Assert
        var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdAtActionResult.ActionName.Should().Be(nameof(DishesController.GetById));
        createdAtActionResult.RouteValues!["restaurantId"].Should().Be(restaurantId);
        createdAtActionResult.RouteValues["dishId"].Should().Be(1);
    }

    [Fact]
    public async Task DeleteDish_ReturnsNoContent()
    {
        // Arrange
        var restaurantId = 1;
        var dishId = 1;
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteDishCommand>(), default))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteDish(restaurantId, dishId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateDish_ReturnsNoContent()
    {
        // Arrange
        var restaurantId = 1;
        var dishId = 1;
        var command = new UpdateDishCommand
        {
            Name = "Updated Dish",
            Description = "Updated Description",
            Price = 14.99m
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateDishCommand>(), default))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateDish(restaurantId, dishId, command);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        command.RestaurantId.Should().Be(restaurantId);
        command.DishId.Should().Be(dishId);
    }
} 