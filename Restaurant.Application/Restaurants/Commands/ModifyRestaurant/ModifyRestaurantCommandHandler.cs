using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.ModifyRestaurant;

public class ModifyRestaurantCommandHandler(ILogger<ModifyRestaurantCommandHandler> logger,
    IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<ModifyRestaurantCommand, bool>
{
    public async Task<bool> Handle(ModifyRestaurantCommand request, CancellationToken cancellationToken)
    {
        var resourceToModify = await restaurantRepository.GetById(request.Id);
        if (resourceToModify is null)
            return false;
        
        mapper.Map(request, resourceToModify);
        await restaurantRepository.SaveChangesAsync();
        return true;
    }
}