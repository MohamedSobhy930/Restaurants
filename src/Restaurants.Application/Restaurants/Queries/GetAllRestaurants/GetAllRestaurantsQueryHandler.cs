using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(IRestaurantsRepo restaurantsRepo,
        ILogger<GetAllRestaurantsQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
    {
        private IRestaurantsRepo _restaurantsRepo = restaurantsRepo;
        private ILogger<GetAllRestaurantsQueryHandler> _logger = logger;
        private IMapper _mapper = mapper;

        public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllRestaurants");
            var (restaurants,totalCount) = await _restaurantsRepo
                .GetAllMatchingAsync(request.SearchPhrase ,
                request.PageNumber ,
                request.PageSize,
                request.SortBy,
                request.SortDirection);
            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            var result = new PagedResult<RestaurantDto>(restaurantDtos,totalCount,request.PageNumber, request.PageSize);
            return result;
        }
    }
}
