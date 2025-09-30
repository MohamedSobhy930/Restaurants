using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Exceptions;
using Restraurants.Domain.Utilities;
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
        private IRestaurantAuthorizationService _restaurantAuthorizationService;
        public DeleteRestaurantCommandHandler(IRestaurantsRepo restaurantsRepo,
            ILogger<DeleteRestaurantCommandHandler> logger,
            IRestaurantAuthorizationService restaurantAuthorizationService,
            IMapper mapper)
        {
            _restaurantsRepo = restaurantsRepo;
            _restaurantAuthorizationService = restaurantAuthorizationService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete a Restaurant with the id {RestaurantId}" , request.Id);
            var restaurant = await _restaurantsRepo.GetByIdAsync(request.Id);
            if (restaurant == null) 
                return false;
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Delete))
                throw new ForbidException();
            await _restaurantsRepo.Delete(restaurant);
            return true;
        }
    }
}
