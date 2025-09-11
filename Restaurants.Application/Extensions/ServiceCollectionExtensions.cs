using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Behaviors;
using Restaurants.Application.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var appAssembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddMediatR(config => config.RegisterServicesFromAssembly(appAssembly));
            services.AddAutoMapper(appAssembly);
            services.AddValidatorsFromAssembly(appAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
