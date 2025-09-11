using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, RestaurantDto>
    {
        private IRestaurantsRepo _restaurantsRepo;
        private ILogger<GetByIdQueryHandler> _logger;
        private IMapper _mapper;
        public GetByIdQueryHandler(IRestaurantsRepo restaurantsRepo,
            ILogger<GetByIdQueryHandler> logger,
            IMapper mapper)
        {
            _restaurantsRepo = restaurantsRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<RestaurantDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Restaurant: {request.id}");
            var restaurant = await _restaurantsRepo.GetByIdAsync(request.id);
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
