using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.API;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;

namespace Restaurants.APITests.Controllers
{
    public class RestaurantsControllerTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRestaurantsRepo> _RestaurantRepo = new();
        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.Replace(ServiceDescriptor.Scoped<IRestaurantsRepo>(_ => _RestaurantRepo.Object));
                    });
            });
        }
        [Fact]
        public async Task GetById_ForNonExistingId_Returns404NotFound()
        {
            // Arrange
            var Id = 123;
            _RestaurantRepo.Setup(x => x.GetByIdAsync(Id)).ReturnsAsync((Restaurant?)null);
            var client = _factory.CreateClient();
            // Act
            var result = await client.GetAsync($"/api/restaurants/{Id}");
            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task GetById_ForExistingId_Returns200OK()
        {
            // Arrange
            var Id = 123;
            var restaurant = new Restaurant { Id = Id, Name = "Test Restaurant" };
            _RestaurantRepo.Setup(x => x.GetByIdAsync(Id)).ReturnsAsync(restaurant);
            var client = _factory.CreateClient();

            // Act
            var result = await client.GetAsync($"/api/restaurants/{Id}");

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetAll_ForValidRequests_Returns200OK()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var result = await client.GetAsync("/api/restaurants?pageNumber=2&pageSize=10");

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetAll_ForInvalidRequests_Returns500BadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var result = await client.GetAsync("/api/restaurants");

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
