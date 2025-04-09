using AutoMapper;
using Restaurant.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurant.Application.Restaurants.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Domain.Entities.Restaurant, RestaurantDto>()
            .ForMember(restaurantDomain => restaurantDomain.City,
                opt => opt.MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.City))
            .ForMember(restaurantDomain => restaurantDomain.Street,
                opt => opt.MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.Street))
            .ForMember(restaurantDomain => restaurantDomain.PostalCode,
                opt => opt.MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.PostalCode))
            .ForMember(restaurantDomain => restaurantDomain.Dishes, opt => opt.MapFrom(restaurant => restaurant.Dishes));

        CreateMap<CreateRestaurantCommand, Domain.Entities.Restaurant>()
            .ForMember(restaurantDomain => restaurantDomain.Address,
                opt => opt.MapFrom(createRestaurantCommand => new Domain.Entities.Address
                {
                    City = createRestaurantCommand.City,
                    Street = createRestaurantCommand.Street,
                    PostalCode = createRestaurantCommand.PostalCode,
                }));
    }
}