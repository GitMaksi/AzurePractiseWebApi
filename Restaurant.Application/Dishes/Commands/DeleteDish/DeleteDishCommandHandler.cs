using MediatR;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.DeleteDish;

public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand>
{
    private readonly IDishRepository _dishRepository;

    public DeleteDishCommandHandler(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        await _dishRepository.DeleteAsync(request.RestaurantId, request.DishId);
    }
} 