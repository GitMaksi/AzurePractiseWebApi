using AutoMapper;
using MediatR;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.GetAllDishes;

public class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, IEnumerable<DishDto>>
{
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;

    public GetAllDishesQueryHandler(IDishRepository dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
        var dishes = await _dishRepository.GetAllForRestaurantAsync(request.RestaurantId);
        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }
} 