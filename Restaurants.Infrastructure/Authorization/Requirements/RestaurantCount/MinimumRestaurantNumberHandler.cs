using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements.RestaurantCount;

internal class MinimumRestaurantNumberHandler(
    IUserContext userContext,
    IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<MinimumRestaurantNumber>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantNumber requirement)
    {
        var user = userContext.GetCurrentUser();
        var restaurants = await restaurantsRepository.GetAllAsync();

        var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == user.Id);

        /* logger.LogInformation("User: {Email} - Handling MinimumRestaurantRequirement",
            user.Email);*/



        if (userRestaurantsCreated >= requirement.Number)
        {
            /*logger.LogInformation("Authorization succeeded");*/
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

    }
}
