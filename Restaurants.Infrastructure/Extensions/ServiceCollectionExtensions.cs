using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.IRepos;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repos;
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

            services.AddScoped<IRestaurantsRepo, RestaurantsRepo>();
        }
    }
}
