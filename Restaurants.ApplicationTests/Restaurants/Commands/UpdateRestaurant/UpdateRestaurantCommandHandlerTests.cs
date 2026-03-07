using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using Restraurants.Domain.Exceptions;
using Restraurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<IRestaurantsRepo> _restaurantsRepoMock;
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantAuthorizationService> _authorizationServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _restaurantsRepoMock = new Mock<IRestaurantsRepo>();
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _authorizationServiceMock = new Mock<IRestaurantAuthorizationService>();
            _mapperMock = new Mock<IMapper>();

            _handler = new UpdateRestaurantCommandHandler(
                _restaurantsRepoMock.Object,
                _loggerMock.Object,
                _authorizationServiceMock.Object,
                _mapperMock.Object);
        }
        [Fact()]
        public async Task handle_ForValidCommand_ReturnsTrue()
        {
            // arrange
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand { Id = restaurantId, Name = "New Name" , Description = "blabla"};
            var existingRestaurant = new Restaurant { Id = restaurantId, Name = "Old Name" , Description = "bloblo"};

            // Mock the repository to return the existing restaurant
            _restaurantsRepoMock.Setup(repo => repo.GetByIdAsync(restaurantId))
                .ReturnsAsync(existingRestaurant);

            // Mock the authorization service to grant access
            _authorizationServiceMock.Setup(auth => auth.Authorize(existingRestaurant, ResourceOperations.Update))
                .Returns(true);

            // act 
            var result =await _handler.Handle(command, CancellationToken.None);
            // assert 
            result.Should().Be(true);

            _mapperMock.Verify(mapper => mapper.Map(command, existingRestaurant), Times.Once);
            _restaurantsRepoMock.Verify(repo => repo.SaveChanges(), Times.Once);
        }
        [Fact()]
        public async Task handle_ForInvalidCommand_ReturnsFalse()
        {
            // arrange
            var nonExistentId = 99;
            var command = new UpdateRestaurantCommand { Id = nonExistentId };

            _restaurantsRepoMock.Setup(repo => repo.GetByIdAsync(nonExistentId))
                .ReturnsAsync((Restaurant?)null);

            // act 
            var result = await _handler.Handle(command, CancellationToken.None);
            // assert 
            result.Should().Be(false);
            _authorizationServiceMock.Verify(auth => auth.Authorize(It.IsAny<Restaurant>(), It.IsAny<ResourceOperations>()), Times.Never);
            _restaurantsRepoMock.Verify(repo => repo.SaveChanges(), Times.Never);
        }
        [Fact]
        public async Task Handle_WhenAuthorizationFails_ShouldThrowForbidException()
        {
            // Arrange
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand { Id = restaurantId };
            var existingRestaurant = new Restaurant { Id = restaurantId };

            // Mock the repository to return the restaurant
            _restaurantsRepoMock.Setup(repo => repo.GetByIdAsync(restaurantId))
                .ReturnsAsync(existingRestaurant);

            // Mock the authorization service to DENY access
            _authorizationServiceMock.Setup(auth => auth.Authorize(existingRestaurant, ResourceOperations.Update))
                .Returns(false);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ForbidException>();
            _restaurantsRepoMock.Verify(repo => repo.SaveChanges(), Times.Never);
        }
    }
}