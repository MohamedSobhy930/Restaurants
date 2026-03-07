using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.IRepos;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants
{
    internal class CreatedMultipleRestaurantRequirementHandler(IRestaurantsRepo restaurants,
        IUserContext usercontext) : AuthorizationHandler<CreatedMultipleRestaurantRequirement>
    {
        private IUserContext _usercontext = usercontext;
        private IRestaurantsRepo _restaurants = restaurants;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantRequirement requirement)
        {
            var user = _usercontext.GetCurrentUser();
            var allRestaurants =await _restaurants.GetAllAsync();
            var restaurantsCount = allRestaurants.Count(r => r.OwnerId == user.Id);

            if (restaurantsCount >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
