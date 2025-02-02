namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string Atleast20 = "Atleast20";
    public const string CreatedAtLeast2Restaurant = "CreatedAtLeast2Restaurant";
}

public static class AppClaimTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
}
