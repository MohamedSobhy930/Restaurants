using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants
{
    public class CreatedMultipleRestaurantRequirement(int minimumRestaurantsCreated) : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; } = minimumRestaurantsCreated;
    }
}
