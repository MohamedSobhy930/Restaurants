using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.IRepos
{
    public interface IRestaurantsRepo 
    {
        public Task<IEnumerable<Restaurant>> GetAllAsync();
        public Task<Restaurant> GetByIdAsync(int id);
        public Task<int> Create(Restaurant restaurant);
        public Task Delete(Restaurant restaurant);
        public Task SaveChanges();
    }
}
