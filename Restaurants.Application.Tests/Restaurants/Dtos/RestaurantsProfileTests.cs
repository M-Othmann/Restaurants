using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Command.CreateRestaurant;
using Restaurants.Application.Restaurants.Command.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Dtos;

public class RestaurantsProfileTests
{
    private IMapper _mapper;

    public RestaurantsProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantsProfile>();
        });

         _mapper = config.CreateMapper();
    }
    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
    {
        //arrange

        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "Test",
            Description = "Test description",
            Category = "Test category",
            HasDelivery = true,
            ContactEmail = "test@tes.com",
            ContactNumber = "123456789",
            Address = new Address
            {
                City = "Test city",
                Street = "Test street",
                PostalCode = "12-345"
            }
        };

        //act

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);


        //assert
        restaurantDto.Should().NotBeNull();
        restaurant.Id.Should().Be(restaurant.Id);
        restaurant.Name.Should().Be(restaurant.Name);
        restaurant.Description.Should().Be(restaurant.Description);
        restaurant.Category.Should().Be(restaurant.Category);
        restaurant.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurant.Address.City.Should().Be(restaurant.Address.City);
        restaurant.Address.Street.Should().Be(restaurant.Address.Street);
        restaurant.Address.PostalCode.Should().Be(restaurant.Address.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForCreateRestaurantToRestaurant_MapsCorrectly()
    {
        //arrange

       

        var command = new CreateRestaurantCommand()
        {

            Name = "Test",
            Description = "Test description",
            Category = "Test category",
            HasDelivery = true,
            ContactEmail = "test@tes.com",
            ContactNumber = "123456789",
            City = "Test city",
            Street = "Test street",
            PostalCode = "12-345"

        };

        //act

        var restaurant = _mapper.Map<Restaurant>(command);


        //assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.Category.Should().Be(command.Category);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
        restaurant.ContactNumber.Should().Be(command.ContactNumber);
        restaurant.ContactEmail.Should().Be(command.ContactEmail);
        restaurant.Address.Should().NotBeNull();
        restaurant.Address.City.Should().Be(command.City);
        restaurant.Address.Street.Should().Be(command.Street);
        restaurant.Address.PostalCode.Should().Be(command.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForUpdateRestaurantToRestaurant_MapsCorrectly()
    {
        //arrange



        var command = new UpdateResturantByIdCommand()
        {
            Id = 1,
            Name = "Update name",
            Description = "Update description",
            HasDelivery = false,
            

        };

        //act

        var restaurant = _mapper.Map<Restaurant>(command);


        //assert
        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(command.Id);
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);


    }


}