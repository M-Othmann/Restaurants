

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        service.AddDbContext<RestaurantsDbContext>(options =>
        options.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());


        service.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        service.AddScoped<IRestaurantsRepository, RestaurantRepository>();
        service.AddScoped<IDishRepository, DishRepository>();
    }
}
