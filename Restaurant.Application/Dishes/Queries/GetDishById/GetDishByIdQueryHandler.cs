using AutoMapper;
using MediatR;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Doman.Exceptions;

namespace Restaurant.Application.Dishes.Queries.GetDishById;

public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishDto>
{
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;

    public GetDishByIdQueryHandler(IDishRepository dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
    }

    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        var dish = await _dishRepository.GetByIdAsync(request.RestaurantId, request.DishId);
        if (dish == null)
        {
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        }

        return _mapper.Map<DishDto>(dish);
    }
} 