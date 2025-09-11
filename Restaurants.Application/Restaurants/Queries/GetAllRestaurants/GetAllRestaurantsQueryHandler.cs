using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        private IRestaurantsRepo _restaurantsRepo;
        private ILogger<GetAllRestaurantsQueryHandler> _logger;
        private IMapper _mapper;
        public GetAllRestaurantsQueryHandler(IRestaurantsRepo restaurantsRepo,
            ILogger<GetAllRestaurantsQueryHandler> logger,
            IMapper mapper)
        {
            _restaurantsRepo = restaurantsRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllRestaurants");
            var restaurants = await _restaurantsRepo.GetAllAsync();
            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantDtos;
        }
    }
}
