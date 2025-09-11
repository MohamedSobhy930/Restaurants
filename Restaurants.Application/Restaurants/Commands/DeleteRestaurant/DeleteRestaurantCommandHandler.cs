using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand , bool>
    {
        private IRestaurantsRepo _restaurantsRepo;
        private ILogger<DeleteRestaurantCommandHandler> _logger;
        private IMapper _mapper;
        public DeleteRestaurantCommandHandler(IRestaurantsRepo restaurantsRepo,
            ILogger<DeleteRestaurantCommandHandler> logger,
            IMapper mapper)
        {
            _restaurantsRepo = restaurantsRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Delete a Restaurant with the id {request.Id}");
            var restaurant = await _restaurantsRepo.GetByIdAsync(request.Id);
            if (restaurant == null) 
                return false;
            await _restaurantsRepo.Delete(restaurant);
            return true;
        }
    }
}
