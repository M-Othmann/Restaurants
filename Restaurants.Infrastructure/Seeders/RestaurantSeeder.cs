using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext _db) : IRestaurantSeeder
{

    public async Task Seed()
    {
        if (await _db.Database.CanConnectAsync())
        {
            if (!_db.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                await _db.Restaurants.AddRangeAsync(restaurants);
                await _db.SaveChangesAsync();
            }

            if (!_db.Roles.Any())
            {
                var roles = GetRoles();
                _db.Roles.AddRange(roles);
                await _db.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            ];
        return roles;
    }


    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [
            new(){
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC is an american fast food restaurant",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes = [
                    new(){
                        Name= "Hot chicken",
                        Description = "Hot chicken (10 pcs.)",
                        Price = 10.30M,
                    },
                    new(){
                        Name= "chicken nugget",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                    ],
                Address = new()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N"
                }
            },

            new (){
                Name = "McDonald",
                Category = "Fast Food",
                Description = "McDonald's incorprated on December 21, 1964",
                ContactEmail = "contact@mac.com",
                HasDelivery = true,
                Address = new Address(){
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F"
                }
            }
            ];

        return restaurants;
    }
}
