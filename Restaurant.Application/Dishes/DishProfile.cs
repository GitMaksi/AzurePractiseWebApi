using AutoMapper;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Dishes;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDto>();
    }
}