using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetAllForRestaurant;
using Restaurants.Domain.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetByIdForRestaurant
{
    public class GetByIdForRestaurantQueryHandler : IRequestHandler<GetByIdForRestaurantQuery, DishDto>
    {
        private IRestaurantsRepo _restaurantsRepo;
        private IDishesRepo _dishRepo;
        private ILogger<GetByIdForRestaurantQueryHandler> _logger;
        private IMapper _mapper;
        public GetByIdForRestaurantQueryHandler(IRestaurantsRepo restaurantsRepo,
            IDishesRepo dishRepo,
            ILogger<GetByIdForRestaurantQueryHandler> logger,
            IMapper mapper)
        {
            _restaurantsRepo = restaurantsRepo;
            _dishRepo = dishRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<DishDto> Handle(GetByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving Dish with id {DishId}, for Restaurant with id {RestaurantId}", request.dishId, request.restaurantId);
            var restaurant = await _restaurantsRepo.GetByIdAsync(request.restaurantId);
            if (restaurant == null)
            {
                return null;
            }
            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.dishId);
            if (dish == null)
                return null;
            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
    }
}
