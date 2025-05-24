using MediatR;

namespace Restaurant.Application.Dishes.Queries.GetDishById;

public record GetDishByIdQuery(int RestaurantId, int DishId) : IRequest<DishDto>; 