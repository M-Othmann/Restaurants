﻿

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Requirements.RestaurantCount;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Configuration;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Storage;

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
        service.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

        service.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "German"))
            .AddPolicy(PolicyNames.Atleast20,
                builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
            .AddPolicy(PolicyNames.CreatedAtLeast2Restaurant,
                builder => builder.AddRequirements(new MinimumRestaurantNumber(2)));

        service.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        service.AddScoped<IAuthorizationHandler, MinimumRestaurantNumberHandler>();

        service.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        service.AddScoped<IBlobStorageService, BlobStorageSerivce>();
    }
}
