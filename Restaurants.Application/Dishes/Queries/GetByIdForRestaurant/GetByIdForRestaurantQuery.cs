using MediatR;
using Restaurants.Application.Dishes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetByIdForRestaurant
{
    public class GetByIdForRestaurantQuery : IRequest<DishDto>
    {
        public GetByIdForRestaurantQuery(int RestaurantId , int DishId) 
        {
            dishId = DishId;
            restaurantId = RestaurantId;
        }
        public int dishId { get; set; }
        public int restaurantId { get; set; }
    }
}
