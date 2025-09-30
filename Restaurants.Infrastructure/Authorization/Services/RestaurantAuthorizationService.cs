using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Interfaces;
using Restraurants.Domain.Entities;
using Restraurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService : IRestaurantAuthorizationService
    {
        private readonly ILogger<RestaurantAuthorizationService> _logger;
        private IUserContext _userContext;
        public RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
            IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }
        public bool Authorize(Restaurant restaurant, ResourceOperations resourceOperation)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("Authorizing {UserEmail} to {Operation} for restaurant {RestaurantName}",
                user.Email, resourceOperation, restaurant.Name);
            if (resourceOperation == ResourceOperations.Read || resourceOperation == ResourceOperations.Create)
            {
                _logger.LogInformation("read/create operation - successful authorization");
                return true;
            }
            if (resourceOperation == ResourceOperations.Delete && user.IsInRole(UserRoles.Admin))
            {
                _logger.LogInformation("admin user , delete operation - successful authorization");
                return true;
            }
            if ((resourceOperation == ResourceOperations.Update || resourceOperation == ResourceOperations.Delete)
                && user.Id == restaurant.OwnerId)
            {
                _logger.LogInformation("restaurant owner - successful authorization");
                return true;
            }
            return false;
        }
    }
}
