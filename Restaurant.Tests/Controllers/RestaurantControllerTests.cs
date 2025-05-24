using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Application.Restaurants.Commands.CreateRestaurant;
using Restaurant.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurant.Application.Restaurants.Commands.ModifyRestaurant;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurant.Application.Restaurants.Queries.GetRestaurantById;
using Restaurant.WebApi.Controllers;

namespace Restaurant.Tests.Controllers;

public class RestaurantControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<RestaurantController>> _loggerMock;
    private readonly RestaurantController _controller;

    public RestaurantControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RestaurantController>>();
        _controller = new RestaurantController(_loggerMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResultWithRestaurants()
    {
        // Arrange
        var restaurants = new List<RestaurantDto>
        {
            new() { Id = 1, Name = "Restaurant 1" },
            new() { Id = 2, Name = "Restaurant 2" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllRestaurantQuery>(), default))
            .ReturnsAsync(restaurants);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedRestaurants = okResult.Value.Should().BeAssignableTo<IEnumerable<RestaurantDto>>().Subject;
        returnedRestaurants.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetById_ReturnsOkResultWithRestaurant()
    {
        // Arrange
        var restaurant = new RestaurantDto { Id = 1, Name = "Test Restaurant" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRestaurantByIdQuery>(), default))
            .ReturnsAsync(restaurant);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedRestaurant = okResult.Value.Should().BeOfType<RestaurantDto>().Subject;
        returnedRestaurant.Id.Should().Be(1);
        returnedRestaurant.Name.Should().Be("Test Restaurant");
    }

    [Fact]
    public async Task CreateRestaurant_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "New Restaurant",
            Description = "Description",
            Category = "Category"
        };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(1);

        // Act
        var result = await _controller.CreateRestaurant(command);

        // Assert
        var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdAtActionResult.ActionName.Should().Be(nameof(RestaurantController.GetById));
        createdAtActionResult.RouteValues!["restaurantId"].Should().Be(1);
    }

    [Fact]
    public async Task DeleteRestaurant_ReturnsNoContent()
    {
        // Arrange
        var restaurantId = 1;
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRestaurantCommand>(), default))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteRestaurant(restaurantId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateRestaurant_ReturnsNoContent()
    {
        // Arrange
        var restaurantId = 1;
        var command = new ModifyRestaurantCommand
        {
            Name = "Updated Restaurant",
            Description = "Updated Description",
            HasDelivery = true
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<ModifyRestaurantCommand>(), default))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateRestaurant(restaurantId, command);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        command.Id.Should().Be(restaurantId);
    }
} 