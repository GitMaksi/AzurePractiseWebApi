using MediatR;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Doman.Exceptions;

namespace Restaurant.Application.Dishes.Commands.UpdateDish;

public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand>
{
    private readonly IDishRepository _dishRepository;

    public UpdateDishCommandHandler(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = await _dishRepository.GetByIdAsync(request.RestaurantId, request.DishId);
        if (dish == null)
        {
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        }

        dish.Name = request.Name;
        dish.Description = request.Description;
        dish.Price = request.Price;
        dish.KiloCalories = request.KiloCalories;

        await _dishRepository.UpdateAsync(dish);
    }
} 