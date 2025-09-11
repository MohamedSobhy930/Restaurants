using MediatR;
using Restaurants.Application.Restaurants.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand : IRequest<bool>
    {
        public DeleteRestaurantCommand(int id)
        {
            this.Id = id;
        }
        public int Id { get; set; }
    }
}
