using Restraurants.Domain.Entities;
using Restraurants.Domain.Utilities;

namespace Restaurants.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperations resourceOperation);
    }
}