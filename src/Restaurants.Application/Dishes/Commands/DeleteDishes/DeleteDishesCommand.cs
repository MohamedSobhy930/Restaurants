using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesCommand : IRequest<bool>
    {
        public DeleteDishesCommand(int restaurantId) 
        { 
            this.restaurantId = restaurantId;
        }
        public int restaurantId { get; set; }
    }
}
