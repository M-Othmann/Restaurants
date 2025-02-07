namespace Restaurants.Infrastructure.Configuration;
public class BlobStorageSettings
{
    public string ConnectionString { get; set; } = default!;
    public string LogosContainer { get; set; } = default!;
}
