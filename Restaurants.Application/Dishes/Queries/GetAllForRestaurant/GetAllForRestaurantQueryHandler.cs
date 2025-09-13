using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetAllForRestaurant
{
    public class GetAllForRestaurantQueryHandler : IRequestHandler<GetAllForRestaurantQuery, IEnumerable<DishDto>>
    {
        private IRestaurantsRepo _restaurantsRepo;
        private IDishesRepo _dishRepo;
        private ILogger<GetAllForRestaurantQueryHandler> _logger;
        private IMapper _mapper;
        public GetAllForRestaurantQueryHandler(IRestaurantsRepo restaurantsRepo,
            IDishesRepo dishRepo,
            ILogger<GetAllForRestaurantQueryHandler> logger,
            IMapper mapper)
        {
            _restaurantsRepo = restaurantsRepo;
            _dishRepo = dishRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DishDto>> Handle(GetAllForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving Dishes for Restaurant with id {RestaurantId}", request.restaurantId);
            var restaurant = await _restaurantsRepo.GetByIdAsync(request.restaurantId);
            if (restaurant == null) {
                return null;
            }
            var dishes = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return dishes;
        }
    }
}
