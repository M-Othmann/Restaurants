using FluentValidation;

namespace Restaurants.Application.Restaurants.Command.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateResturantByIdCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 100);
    }

}
