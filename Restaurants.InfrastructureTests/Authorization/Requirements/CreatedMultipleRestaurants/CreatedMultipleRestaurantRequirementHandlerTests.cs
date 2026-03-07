using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.IRepos;
using Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants.Tests
{
    public class CreatedMultipleRestaurantRequirementHandlerTests
    {
        private readonly Mock<IRestaurantsRepo> _restaurantsRepoMock;
        private readonly Mock<IUserContext> _userContextMock;

        public CreatedMultipleRestaurantRequirementHandlerTests()
        {
            _restaurantsRepoMock = new Mock<IRestaurantsRepo>();
            _userContextMock = new Mock<IUserContext>();
        }
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
        {
            // arrange 
            var currentUser = new CurrentUser("1", "test@test.com", [],null,null);
            _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>
            {
                new Restaurant { OwnerId = "1" },       
                new Restaurant { OwnerId = "1" },       
                new Restaurant { OwnerId = "other-user" }  
            };
            _restaurantsRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new CreatedMultipleRestaurantRequirement(2);
            var handler = new CreatedMultipleRestaurantRequirementHandler(_restaurantsRepoMock.Object, _userContextMock.Object);
            var context = new AuthorizationHandlerContext([requirement], null, null);

            // act 
            await handler.HandleAsync(context);

            // assert 
            context.HasSucceeded.Should().BeTrue();
        }
        [Fact]
        public async Task HandleRequirementAsync_UserHasNotCreatedEnoughRestaurants_ShouldFail()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>
            {
                new Restaurant { OwnerId = "1" },      
                new Restaurant { OwnerId = "2" } 
            };
            _restaurantsRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new CreatedMultipleRestaurantRequirement(2);
            var handler = new CreatedMultipleRestaurantRequirementHandler(_restaurantsRepoMock.Object, _userContextMock.Object);
            var context = new AuthorizationHandlerContext([requirement], null, null);

            // Act
            await handler.HandleAsync(context);

            // Assert
            context.HasSucceeded.Should().BeFalse();
        }
    }
}