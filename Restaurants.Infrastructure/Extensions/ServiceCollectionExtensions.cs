using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.IRepos;
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

            services.AddIdentityCore<User>(options => { })
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddHttpContextAccessor();
            services.AddSingleton<TimeProvider>(TimeProvider.System);
            services.AddSingleton<IEmailSender<User>, NoOpEmailSender<User>>();
            services.AddScoped<IRestaurantsRepo, RestaurantsRepo>();
            services.AddScoped<IDishesRepo, DishesRepo>();
        }
    }
}
