

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
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


        service.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipleFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        service.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        service.AddScoped<IRestaurantsRepository, RestaurantRepository>();
        service.AddScoped<IDishRepository, DishRepository>();

        service.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "German"));
    }
}
