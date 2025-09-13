using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Dishes.Commands.CreateDish.CreateRestaurant;
using Restaurants.Domain.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesCommandHandler : IRequestHandler<DeleteDishesCommand, bool>
    {
        private IDishesRepo _dishesRepo;
        private IRestaurantsRepo _restaurantsRepo;
        private ILogger<DeleteDishesCommandHandler> _logger;
        private IMapper _mapper;
        public DeleteDishesCommandHandler(IDishesRepo dishesRepo,
            IRestaurantsRepo restaurantsRepo,
            ILogger<DeleteDishesCommandHandler> logger,
            IMapper mapper)
        {
            _dishesRepo = dishesRepo;
            _restaurantsRepo = restaurantsRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Dishes For restaurant with Id {RestaurantId}", request.restaurantId);
            var restaurant =await _restaurantsRepo.GetByIdAsync(request.restaurantId);
            if (restaurant == null)
                return false;
            await _dishesRepo.Delete(restaurant.Dishes);
            return true;

        }
    }
}
