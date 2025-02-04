using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Command.CreateRestaurant;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Command.CreateRestaurant
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async void Handle_ForValidCommand_ReturnsCreateRestaurantId()
        {
            //arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();

            mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);


            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<Restaurant>()))
                .ReturnsAsync(1);


            var userContext = new Mock<IUserContext>();
            var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
            userContext.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var commandHandler = new CreateRestaurantCommandHandler(
                loggerMock.Object,
                mapperMock.Object,
                restaurantRepositoryMock.Object,
                userContext.Object
                );

            //act
            var result = await commandHandler.Handle(command, CancellationToken.None);


            //assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("owner-id");
            restaurantRepositoryMock.Verify(r => r.Create(restaurant), Times.Once);
        }
    }
}