using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Users;


namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection service)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        service.AddAutoMapper(applicationAssembly);
        service.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        service.AddScoped<IUserContext, UserContext>();
        service.AddHttpContextAccessor();
    }

}
