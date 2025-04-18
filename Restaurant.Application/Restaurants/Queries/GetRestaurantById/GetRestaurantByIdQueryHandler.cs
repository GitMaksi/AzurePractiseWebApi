using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurant.Domain.Repositories;
using Restaurant.Doman.Exceptions;

namespace Restaurant.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetAllRestaurantQueryHandler> logger,  IMapper mapper, IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get restaurant function started, id: {request.RestaurantId}");
        var restaurant = await restaurantRepository.GetById(request.RestaurantId) 
         ?? throw new NotFoundException(nameof(Domain.Entities.Restaurant),
             request.RestaurantId.ToString());
        
        logger.LogInformation($"Get restaurant function finished, id: {request.RestaurantId}");
        return mapper.Map<RestaurantDto>(restaurant);
    }
}