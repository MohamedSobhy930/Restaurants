using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepos;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Identity;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repos;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantDb");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddHttpContextAccessor();
            services.AddSingleton<TimeProvider>(TimeProvider.System);
            services.AddSingleton<IEmailSender<User>, NoOpEmailSender<User>>();
            services.AddScoped<IRestaurantsRepo, RestaurantsRepo>();
            services.AddScoped<IDishesRepo, DishesRepo>();

            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "egyptian"))
                .AddPolicy(PolicyNames.Atleast20, builder => builder.AddRequirements( new MinimumAgeRequirement(21)))
                .AddPolicy(PolicyNames.CreatedAtleast2Restaurants, buidler => buidler.AddRequirements(new CreatedMultipleRestaurantRequirement(2)));
                
            services.AddScoped<IAuthorizationHandler , MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantRequirementHandler>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        }
    }
}
