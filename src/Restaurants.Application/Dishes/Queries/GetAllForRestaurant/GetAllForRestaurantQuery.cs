using MediatR;
using Restaurants.Application.Dishes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetAllForRestaurant
{
    public class GetAllForRestaurantQuery: IRequest<IEnumerable<DishDto>>
    {
        public GetAllForRestaurantQuery(int restaurantId)
        {
            this.restaurantId = restaurantId;
        }
        public int restaurantId { get; set; }
    }
}
