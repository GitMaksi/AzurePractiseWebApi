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
        _ = await restaurantRepository.GetById(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Restaurant),
                request.Id.ToString());
        
        await restaurantRepository.DeleteAsync(request.Id);
        logger.LogInformation($"Resource  deleted, id: {request.Id}");
    }
}