using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();

    Task<Restaurant?> GetRestaurant(int id);

    Task<int> Create(Restaurant entity);

    Task Delete(Restaurant entity);

    Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);

    Task SaveChanges();
}
