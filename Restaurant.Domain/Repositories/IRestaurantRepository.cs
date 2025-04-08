namespace Restaurant.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Entities.Restaurant>> AllAsync();
    Task<Entities.Restaurant?> GetById(int id);
}