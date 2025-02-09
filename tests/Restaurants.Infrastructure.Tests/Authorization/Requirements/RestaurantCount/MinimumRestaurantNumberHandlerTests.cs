﻿using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Infrastructure.Authorization.Requirements.RestaurantCount.Tests
{
    public class MinimumRestaurantNumberHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(m => m.GetCurrentUser())
                .Returns(currentUser);

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };

            var restaurantsRepoMock = new Mock<IRestaurantsRepository>();
            restaurantsRepoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(restaurants);

            var requirment = new MinimumRestaurantNumber(2);

            var handler = new MinimumRestaurantNumberHandler(userContextMock.Object,
                restaurantsRepoMock.Object);

            var context = new AuthorizationHandlerContext([requirment], null, null);


            //act
            await handler.HandleAsync(context);

            //assert
            context.HasSucceeded.Should().BeTrue();
        }


        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(m => m.GetCurrentUser())
                .Returns(currentUser);

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };

            var restaurantsRepoMock = new Mock<IRestaurantsRepository>();
            restaurantsRepoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(restaurants);

            var requirment = new MinimumRestaurantNumber(2);

            var handler = new MinimumRestaurantNumberHandler(userContextMock.Object,
                restaurantsRepoMock.Object);

            var context = new AuthorizationHandlerContext([requirment], null, null);


            //act
            await handler.HandleAsync(context);

            //assert
            context.HasSucceeded.Should().BeFalse();
        }
    }
}