using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restraurants.Domain.Utilities;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrenUser()
        {
            //arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>
            { 
                new (ClaimTypes.NameIdentifier, "1"),
                new (ClaimTypes.Email, "test@test.com"),
                new (ClaimTypes.Role, UserRoles.Admin),
                new (ClaimTypes.Role, UserRoles.User),
                new ("Nationality", "egyptian"),
                new ("DateOfBirth", new DateOnly(2000,1,1).ToString("yyyy-MM-dd"))
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });
            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            var currentUser = userContext.GetCurrentUser();

            //assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin,UserRoles.User);
            currentUser.Nationality.Should().Be("egyptian");
            currentUser.DateOfBirth.Should().Be(new DateOnly(2000, 1, 1));
        }
        [Fact()]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            // arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
            var userContext = new UserContext(httpContextAccessorMock.Object);
            
            // act 
            Action act = () => userContext.GetCurrentUser();
            
            // assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("user context is not present");
        }
    }
}