using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository(RestaurantsDbContext dbContext) : IDishRepository
{
    public async Task<int> Create(Dish entity)
    {
        dbContext.Dishes.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Dish entity)
    {
        dbContext.Dishes.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}
