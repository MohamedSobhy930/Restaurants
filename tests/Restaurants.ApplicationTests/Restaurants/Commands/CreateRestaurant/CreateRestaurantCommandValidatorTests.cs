using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationError()
        {
            //arrange 
            var command = new CreateRestaurantCommand()
            {
                Name = "test",
                Description = "blablabla",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PhoneNumber = "12345678901",
                PostalCode = "123-456",
            };
            var validator = new CreateRestaurantCommandValidator();
            //act 
            var result = validator.TestValidate(command);
            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidationError()
        {
            //arrange 
            var command = new CreateRestaurantCommand()
            {
                Name = "e",
                Description = "",
                Category = "England",
                ContactEmail = "test@",
                PhoneNumber = "1234567890",
                PostalCode = "123456",
            };
            var validator = new CreateRestaurantCommandValidator();
            //act 
            var result = validator.TestValidate(command);
            //assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Category);
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }
        //testing category
        [Theory]
        [InlineData(null)]          
        [InlineData("")]            
        [InlineData("  ")]          
        [InlineData("French")]      
        [InlineData("chinese")]     
        public void Validator_ForInvalidCategory_ShouldHaveValidationError(string category)
        {
            // Arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Test Restaurant",
                Description = "A great place to eat.",
                ContactEmail = "test@example.com",
                PhoneNumber = "12345678901",
                PostalCode = "123-456",

                Category = category
            };
            var validator = new CreateRestaurantCommandValidator();
            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Category);
        }
    }
}