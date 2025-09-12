using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private IRestaurantsRepo _restaurantsRepo;
        private ILogger<UpdateRestaurantCommandHandler> _logger;
        private IMapper _mapper;
        public UpdateRestaurantCommandHandler(IRestaurantsRepo restaurantsRepo,
            ILogger<UpdateRestaurantCommandHandler> logger,
            IMapper mapper)
        {
            _restaurantsRepo = restaurantsRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create a new Restaurant{@Restaurant}" , request);
            var restaurant = _mapper.Map<Restaurant>(request);
            int id = await _restaurantsRepo.Create(restaurant);
            return id;
        }
    }
}
