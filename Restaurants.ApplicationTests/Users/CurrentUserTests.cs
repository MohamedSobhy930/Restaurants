using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restraurants.Domain.Utilities;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        // naming convention => TestMethod_Scenario_ExpectedResult
        //[Fact()]
        [Theory]
        [InlineData(UserRoles.User)]
        [InlineData(UserRoles.Admin)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //arrange
            var user = new CurrentUser("1", "test@test.com", [UserRoles.User, UserRoles.Admin], null, null);
            //act
            var isInRole = user.IsInRole(roleName);
            //assert
            isInRole.Should().BeTrue();
        }
        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //arrange
            var user = new CurrentUser("1", "test@test.com", [UserRoles.User, UserRoles.Admin], null, null);
            //act
            var isInRole = user.IsInRole(UserRoles.Owner);
            //assert
            isInRole.Should().BeFalse();
        }

    }
}