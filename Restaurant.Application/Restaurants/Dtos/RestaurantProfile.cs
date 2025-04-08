using AutoMapper;

namespace Restaurant.Application.Restaurants.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Domain.Entities.Restaurant, RestaurantDto>()
            .ForMember(restaurant => restaurant.City,
                opt => opt.MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.City))
            .ForMember(restaurant => restaurant.Street,
                opt => opt.MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.Street))
            .ForMember(restaurant => restaurant.PostalCode,
                opt => opt.MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.PostalCode))
            .ForMember(restaurant => restaurant.Dishes, opt => opt.MapFrom(restaurant => restaurant.Dishes));

    }
}