using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.DTOs
{
    public class DishProfile : Profile
    {
        public DishProfile() 
        {
            CreateMap<Dish, DishDto>();
            CreateMap<CreateDishCommand, Dish>();
        }
    }
}
