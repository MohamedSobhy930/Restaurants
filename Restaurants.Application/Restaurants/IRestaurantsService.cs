using Restaurants.Application.Restaurants.DTOs;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        public Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
        public Task<RestaurantDto> GetRestaurant(int id);
        public Task<int> Create(CreateRestaurantDto restaurant);
    }
}
