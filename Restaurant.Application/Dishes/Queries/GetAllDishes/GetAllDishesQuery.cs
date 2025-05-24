using MediatR;

namespace Restaurant.Application.Dishes.Queries.GetAllDishes;

public record GetAllDishesQuery(int RestaurantId) : IRequest<IEnumerable<DishDto>>; 