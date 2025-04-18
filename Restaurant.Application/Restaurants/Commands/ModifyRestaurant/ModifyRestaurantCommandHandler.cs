using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;
using Restaurant.Doman.Exceptions;

namespace Restaurant.Application.Restaurants.Commands.ModifyRestaurant;

public class ModifyRestaurantCommandHandler(ILogger<ModifyRestaurantCommandHandler> logger,
    IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<ModifyRestaurantCommand>
{
    public async Task Handle(ModifyRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Modify restaurant function started, id: {request.Id}");
        var resourceToModify = await restaurantRepository.GetById(request.Id)
            ?? throw new NotFoundException($"Resource {request.Id} not found");
        
        mapper.Map(request, resourceToModify);
        await restaurantRepository.SaveChangesAsync();
        logger.LogInformation($"Modify restaurant function finished, id: {request.Id}");
    }
}