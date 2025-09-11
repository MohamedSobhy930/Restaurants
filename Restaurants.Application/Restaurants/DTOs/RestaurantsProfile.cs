using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, opt =>
                opt.MapFrom(src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                }));
            CreateMap<UpdateRestaurantCommand, Restaurant>();
                
            CreateMap<Restaurant,RestaurantDto>()
                .ForMember(d => d.City , opt => opt.MapFrom(src => src.Address.City))
                .ForMember(d => d.Street , opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(d => d.PostalCode , opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(d => d.Dishes , opt => opt.MapFrom(src => src.Dishes));
 
        }
    }
}
