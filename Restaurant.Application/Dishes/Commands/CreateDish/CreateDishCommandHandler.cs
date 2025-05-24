using AutoMapper;
using MediatR;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
{
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;

    public CreateDishCommandHandler(IDishRepository dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = new Dish
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            KiloCalories = request.KiloCalories,
            RestaurantId = request.RestaurantId
        };

        return await _dishRepository.CreateAsync(dish);
    }
} 