using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using Restraurants.Domain.Exceptions;
using Restraurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(IRestaurantsRepo restaurantsRepo,
        ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantAuthorizationService restaurantAuthorizationService,
        IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private IRestaurantsRepo _restaurantsRepo = restaurantsRepo;
        private ILogger<UpdateRestaurantCommandHandler> _logger = logger;
        private IRestaurantAuthorizationService _restaurantAuthorizationService = restaurantAuthorizationService;
        private IMapper _mapper = mapper;

        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Restaurant With the Id {RestaurantId} , {@UpdateRestaurant}", request.Id , request);
            Restaurant restaurant =await _restaurantsRepo.GetByIdAsync(request.Id);
            if( restaurant == null ) 
                return false;
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Update))
                throw new ForbidException();

            _mapper.Map(request, restaurant);
            await _restaurantsRepo.SaveChanges();
            return true;
        }
    }
}
