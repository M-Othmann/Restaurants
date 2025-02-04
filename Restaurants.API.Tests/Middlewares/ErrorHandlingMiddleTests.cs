using Xunit;
using Restaurants.API.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Restaurants.API.Middlewares.Tests
{
    public class ErrorHandlingMiddleTests
    {
        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
        {
            //arrange

            var loggerMock = new Mock<ILogger<ErrorHandlingMiddle>>();
            var middleware = new ErrorHandlingMiddle(loggerMock.Object);
            var context = new DefaultHttpContext();
            var nextDelegate = new Mock<RequestDelegate>();

            //ACT
            await middleware.InvokeAsync(context, nextDelegate.Object);

            //assert
            nextDelegate.Verify(next => next.Invoke(context), Times.Once);



        }

        [Fact()]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCodeTo404()
        {
            //Arrange

            var loggerMock = new Mock<ILogger<ErrorHandlingMiddle>>();
            var middleware = new ErrorHandlingMiddle(loggerMock.Object);
            var context = new DefaultHttpContext();
            var notFoundException = new NotFoundException(nameof(Restaurant), "1");

            //Act

            await middleware.InvokeAsync(context, _ => throw notFoundException);

            //Assert
            context.Response.StatusCode.Should().Be(404);


        }

        [Fact()]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCodeTo403()
        {
            //Arrange

            var loggerMock = new Mock<ILogger<ErrorHandlingMiddle>>();
            var middleware = new ErrorHandlingMiddle(loggerMock.Object);
            var context = new DefaultHttpContext();
            var forbidException = new ForbidException();

            //Act

            await middleware.InvokeAsync(context, _ => throw forbidException);

            //Assert
            context.Response.StatusCode.Should().Be(403);


        }
        [Fact()]
        public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCodeTo500()
        {
            //Arrange

            var loggerMock = new Mock<ILogger<ErrorHandlingMiddle>>();
            var middleware = new ErrorHandlingMiddle(loggerMock.Object);
            var context = new DefaultHttpContext();
            var genericException = new Exception();

            //Act

            await middleware.InvokeAsync(context, _ => throw genericException);

            //Assert
            context.Response.StatusCode.Should().Be(500);


        }

    }
}