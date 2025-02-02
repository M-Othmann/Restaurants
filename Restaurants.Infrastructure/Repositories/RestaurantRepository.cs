﻿using Microsoft.EntityFrameworkCore;
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

    public async Task Delete(Restaurant entity)
    {
        _db.Restaurants.Remove(entity);
        await _db.SaveChangesAsync();


    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await _db.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = _db
            .Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        var restaurants = await baseQuery
             .Skip(pageSize * (pageNumber - 1))
             .Take(pageNumber)
             .ToListAsync();




        return (restaurants, totalCount);
    }

    public async Task<Restaurant?> GetRestaurant(int id)
    {
        var restaurant = await _db.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
        return restaurant;
    }

    public Task SaveChanges() => _db.SaveChangesAsync();



}
