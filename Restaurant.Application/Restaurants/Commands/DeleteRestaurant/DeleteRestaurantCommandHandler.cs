using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;
using Restaurant.Doman.Exceptions;

namespace Restaurant.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository)
    : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Delete resource");
        var resourceToDelete = await restaurantRepository.GetById(request.Id)
            ?? throw new NotFoundException($"Resource {request.Id} not found");
        
        await restaurantRepository.DeleteAsync(request.Id);
        logger.LogInformation($"Resource  deleted, id: {request.Id}");
    }
}