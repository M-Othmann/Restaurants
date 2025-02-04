using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Command.UpdateRestaurant.Tests;

public class UpdateResturantByIdCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateResturantByIdCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;

    private readonly UpdateResturantByIdCommandHandler _handler;

    public UpdateResturantByIdCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateResturantByIdCommandHandler>>();
        _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        _mapperMock = new Mock<IMapper>();
        _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        _handler = new UpdateResturantByIdCommandHandler(
            _loggerMock.Object,
            _restaurantsRepositoryMock.Object,
            _mapperMock.Object,
            _restaurantAuthorizationServiceMock.Object
            );
    }


    [Fact()]
    public async void Handle_WithValidRequest_ShouldUpdateRestaurant()
    {
        var restaurantId = 1;
        var command = new UpdateResturantByIdCommand()
        {
            Id = restaurantId,
            Name = "New test",
            Description = "New description",
            HasDelivery = true,
        };

        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test",
        };

        _restaurantsRepositoryMock.Setup(r => r.GetRestaurant(restaurantId))
            .ReturnsAsync(restaurant);

        _restaurantAuthorizationServiceMock.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
            .Returns(true);


        //act
        await _handler.Handle(command, CancellationToken.None);

        //assert

        _restaurantsRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);


    }


    [Fact()]
    public async void Handle_WithNonExistingRestaurant_ReturnsNotFoundException()
    {
        //arrange
        var restaurantId = 2;
        var request = new UpdateResturantByIdCommand()
        {
            Id = restaurantId
        };

        _restaurantsRepositoryMock.Setup(r => r.GetRestaurant(restaurantId))
            .ReturnsAsync((Restaurant?)null);

        //act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id: {restaurantId} does not exist");

    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        //arrange
        var restaurantId = 3;
        var request = new UpdateResturantByIdCommand
        {
            Id = restaurantId
        };

        var existingRestaurant = new Restaurant
        {
            Id = restaurantId
        };

        _restaurantsRepositoryMock
            .Setup(r => r.GetRestaurant(restaurantId))
            .ReturnsAsync(existingRestaurant);

        _restaurantAuthorizationServiceMock.Setup(m => m.Authorize(existingRestaurant, Domain.Constants.ResourceOperation.Update))
           .Returns(false);

        //act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<ForbidException>();
    }
}