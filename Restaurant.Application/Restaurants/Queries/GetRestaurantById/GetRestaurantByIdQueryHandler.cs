using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetAllRestaurantQueryHandler> logger,  IMapper mapper, IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get restaurant function started, id: {request.Id}");
        var restaurant = await restaurantRepository.GetById(request.Id);
        logger.LogInformation($"Get restaurant function finished, id: {request.Id}");
        return mapper.Map<RestaurantDto?>(restaurant);
    }
}