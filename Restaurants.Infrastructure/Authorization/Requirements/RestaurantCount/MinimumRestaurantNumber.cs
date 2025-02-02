using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.RestaurantCount;

public class MinimumRestaurantNumber(int MinimumRestaurantNumber) : IAuthorizationRequirement
{
    public int Number { get; } = MinimumRestaurantNumber;
}
