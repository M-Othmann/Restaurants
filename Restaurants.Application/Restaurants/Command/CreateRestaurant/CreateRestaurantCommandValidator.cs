using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace Restaurants.Application.Restaurants.Command.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{

    private readonly List<string> _validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);



        RuleFor(dto => dto.Category)
            .Must(_validCategories.Contains)
            .WithMessage("Invalid Category");
        /*.Custom((value, context) =>
        {
            var isValid = _validCategories.Contains(value);
            if (!isValid)
                context.AddFailure("Category", "Invalid Category");

        });*/

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.PostalCode)
        .Matches(@"^\d{2}-\d{3}")
            .WithMessage("Please enter valid Postal code(XX - XXX)");
    }
}
