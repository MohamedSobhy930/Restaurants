using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using Restaurants.Application.Users;
using FluentAssertions;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            // arrange 
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();
            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
            mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

            var restaurantRepoMock = new Mock<IRestaurantsRepo>();
            restaurantRepoMock.Setup(r => r.Create(It.IsAny<Restaurant>())).ReturnsAsync(1);

            var currentUserMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("owner_id", "test@test.com", [], null, null);
            currentUserMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);    

            var commandHandler = new CreateRestaurantCommandHandler
                (restaurantRepoMock.Object,loggerMock.Object,mapperMock.Object,currentUserMock.Object);
            // act 
            var result = await commandHandler.Handle(command, CancellationToken.None);
            // assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("owner_id");
            restaurantRepoMock.Verify(r => r.Create(restaurant), Times.Once);
        }

    }
}