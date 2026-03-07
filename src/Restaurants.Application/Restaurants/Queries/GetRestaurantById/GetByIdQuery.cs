using MediatR;
using Restaurants.Application.Restaurants.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetByIdQuery : IRequest<RestaurantDto>
    {
        public GetByIdQuery(int id) 
        {
            this.id = id;
        }
        public int id { get; set; }
    }
}
