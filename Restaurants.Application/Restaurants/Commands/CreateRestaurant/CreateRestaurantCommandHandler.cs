using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Users;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private IRestaurantsRepo _restaurantsRepo;
        private ILogger<CreateRestaurantCommandHandler> _logger;
        private IMapper _mapper;
        private IUserContext _usercontext;
        public CreateRestaurantCommandHandler(IRestaurantsRepo restaurantsRepo,
            ILogger<CreateRestaurantCommandHandler> logger,
            IMapper mapper, IUserContext usercontext)
        {
            _restaurantsRepo = restaurantsRepo;
            _logger = logger;
            _mapper = mapper;
            _usercontext = usercontext;
        }
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var user = _usercontext.GetCurrentUser();
            _logger.LogInformation("{userEmail} is Creating a new Restaurant{@Restaurant}" ,user.Email, request);
            var restaurant = _mapper.Map<Restaurant>(request);
            restaurant.OwnerId = user.Id;
            int id = await _restaurantsRepo.Create(restaurant);
            return id;
        }
    }
}
