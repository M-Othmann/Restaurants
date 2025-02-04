using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Command.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidComman_ShouldNotHaveValidationError()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "12-345"
        };

        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);


        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidComman_ShouldHaveValidationError()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Category = "Ita",
            ContactEmail = "@test.com",
            PostalCode = "12345"
        };

        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);


        //assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }


    [Theory()]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("American")]
    [InlineData("Indian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        //arrrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { Category = category };

        //act
        var result = validator.TestValidate(command);


        //assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 202")]
    [InlineData("10-0 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        //arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { PostalCode = postalCode };


        //act
        var result = validator.TestValidate(command);


        //assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);

    }
}