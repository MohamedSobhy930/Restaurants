using FluentValidation;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Restaurants.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator() 
        {
            RuleFor(x => x.Name)
                .Length(3, 100);
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price Must be Greater Than 0");
            
        }
    }
}
