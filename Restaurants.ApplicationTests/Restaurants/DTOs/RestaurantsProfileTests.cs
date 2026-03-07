using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.Application.Restaurants.DTOs.Tests
{
    public class RestaurantsProfileTests
    {
        //[Fact]
        //public void Configuration_ShouldBeValid()
        //{
        //    //arrange
        //    var configuration = new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile<RestaurantsProfile>();
        //    });
        //    // Act & Assert

        //    configuration.AssertConfigurationIsValid();
        //}
        [Fact]
        public void Restaurant_To_RestaurantDto_MapsCorrectly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });
            var mapper = configuration.CreateMapper();

            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Abou Tarek",
                Description = "koushary",
                Category = "Egyptian",
                HasDelivery = true,
                ContactEmail = "tarek@test.com",
                PhoneNumber = "12345678901",
                Address = new Address
                {
                    City = "Cairo",
                    Street = "Champollion St",
                    PostalCode = "11111"
                }
            };

            // Act
            var dto = mapper.Map<RestaurantDto>(restaurant);

            // Assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(restaurant.Id);
            dto.Name.Should().Be(restaurant.Name);
            dto.Category.Should().Be(restaurant.Category);
            dto.Description.Should().Be(restaurant.Description);
            dto.HasDelivery.Should().Be(restaurant.HasDelivery);
            dto.City.Should().Be(restaurant.Address.City);
            dto.Street.Should().Be(restaurant.Address.Street);
            dto.PostalCode.Should().Be(restaurant.Address.PostalCode);

        }
        [Fact]
        public void CreateRestaurantCommand_To_Restaurant_MapsCorrectly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });
            var mapper = configuration.CreateMapper();

            var command = new CreateRestaurantCommand
            {
                Name = "Abou Tarek",
                Description = "koushary",
                Category = "Egyptian",
                HasDelivery = true,
                ContactEmail = "tarek@test.com",
                PhoneNumber = "12345678901",
                City = "Cairo",
                Street = "Champollion St",
                PostalCode = "11111"
            };

            // Act
            var restaurant = mapper.Map<Restaurant>(command);

            // Assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Category.Should().Be(command.Category);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
            restaurant.Address.Should().NotBeNull();
            restaurant.Address.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        }
        [Fact]
        public void UpdateRestaurantCommand_To_Restaurant_MapsCorrectly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });
            var mapper = configuration.CreateMapper();

            var command = new UpdateRestaurantCommand
            {
                Id = 1,
                Name = "Abou Tarek",
                Description = "koushary",
                HasDelivery = true,
            };

            // Act
            var restaurant = mapper.Map<Restaurant>(command);

            // Assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
        }
    }
}