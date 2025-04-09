using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
    ILogger<CreateRestaurantCommandHandler> logger, IMapper  mapper)
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Creating resource");
        var restaurantDomainModel = mapper.Map<Domain.Entities.Restaurant>(request);
        logger.LogInformation($"Mapped resource completed");
        return await restaurantRepository.CreateAsync(restaurantDomainModel);
    }
}