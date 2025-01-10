using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantRepository(RestaurantsDbContext _db)
    : IRestaurantsRepository
{
    public async Task<int> Create(Restaurant entity)
    {
        await _db.Restaurants.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await _db.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetRestaurant(int id)
    {
        var restaurant = await _db.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
        return restaurant;
    }
}
