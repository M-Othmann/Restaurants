using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Insert valid category");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.PostalCode)
        .Matches(@"^\d{2}-\d{3}")
            .WithMessage("Please enter valid Postal code(XX - XXX)");
    }
}
