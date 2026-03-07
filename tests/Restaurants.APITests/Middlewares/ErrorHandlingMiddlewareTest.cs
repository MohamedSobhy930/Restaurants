using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.API.Middlewares;
using Restraurants.Domain.Exceptions;

namespace Restaurants.APITests.Middlewares
{
    public class ErrorHandlingMiddlewareTest
    {
        [Fact()]
        public async Task InvokeAsyncTest_WhenNoException_ShouldCallNextDelegate()
        {
            //arrange 
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var nextDelegateMock = new Mock<RequestDelegate>();

            //act 
            await middleware.InvokeAsync(context , nextDelegateMock.Object);

            //assert
            nextDelegateMock.Verify(next => next(context), Times.Once);
        }
        [Fact()]
        public async Task InvokeAsyncTest_WhenForbidException_ShouldReturn403()
        {
            //arrange 
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var exception = new ForbidException();


            //act 
            await middleware.InvokeAsync(context, _ => throw exception);

            //assert
            context.Response.StatusCode.Should().Be(403);
        }
        [Fact()]
        public async Task InvokeAsyncTest_WhenException_ShouldReturn500()
        {
            //arrange 
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var exception = new Exception();


            //act 
            await middleware.InvokeAsync(context, _ => throw exception);

            //assert
            context.Response.StatusCode.Should().Be(500);
        }
    }
}
