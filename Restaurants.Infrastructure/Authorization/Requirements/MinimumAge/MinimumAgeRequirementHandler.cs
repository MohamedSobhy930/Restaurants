using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAge
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private ILogger<MinimumAgeRequirementHandler> _logger;
        private IUserContext _usercontext;
        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger ,
            IUserContext usercontext) {
            _logger = logger;
            _usercontext = usercontext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var user = _usercontext.GetCurrentUser();
            _logger.LogInformation("User : {Email} , {DoB} Handling Minimum Age Requirement", user.Email, user.DateOfBirth);

            if(user.DateOfBirth == null)
            {
                _logger.LogWarning("DateOfBirth is null");
                context.Fail();
                return Task.CompletedTask;
            }
            if (user.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
