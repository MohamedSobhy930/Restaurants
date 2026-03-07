using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowedPages = { 5, 10, 15, 20 };
        private string[] allowedSortBy = { nameof(RestaurantDto.Name), nameof(RestaurantDto.Category) , nameof(RestaurantDto.Description) };
        public GetAllRestaurantsQueryValidator() 
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("page number should be greater than 1");
            RuleFor(r => r.PageSize)
                .Must(p => allowedPages.Contains(p))
                .WithMessage($"Page size should be in {string.Join(",", allowedPages)}");
            RuleFor(r => r.SortBy)
                .Must(p => allowedSortBy.Contains(p))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort By should be in {string.Join(",", allowedSortBy)}");

        }
    }
}
