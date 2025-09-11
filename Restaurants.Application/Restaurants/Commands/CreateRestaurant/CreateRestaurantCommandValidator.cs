using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator() 
        {
            RuleFor(x => x.Name)
                .Length(3, 100);
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Enter a valid Category");
            RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Enter a valid Email address");
            RuleFor(x => x.PhoneNumber)
                .Matches(@"\d{11}$").WithMessage("Enter a valid Phone number");
            RuleFor(x => x.PostalCode)
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Enter a valid postal Code (XX-XXX)");
        }
    }
}
