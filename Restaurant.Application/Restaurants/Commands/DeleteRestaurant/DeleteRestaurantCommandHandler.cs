using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository)
    : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Delete resource");
        var resourceToDelete = await restaurantRepository.GetById(request.Id);

        if (resourceToDelete is null)
            return false;
        
        await restaurantRepository.DeleteAsync(request.Id);
        logger.LogInformation($"Resource  deleted, id: {request.Id}");
        
        return true;
    }
}