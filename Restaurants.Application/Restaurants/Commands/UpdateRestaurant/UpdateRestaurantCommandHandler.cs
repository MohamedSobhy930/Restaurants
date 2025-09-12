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

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
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
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Restaurant With the Id {RestaurantId} , {@UpdateRestaurant}", request.Id , request);
            Restaurant restaurant =await _restaurantsRepo.GetByIdAsync(request.Id);
            if( restaurant == null ) 
                return false;

            _mapper.Map(request, restaurant);
            await _restaurantsRepo.SaveChanges();
            return true;
        }
    }
}
