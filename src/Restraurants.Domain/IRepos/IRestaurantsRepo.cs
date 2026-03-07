using Restraurants.Domain.Entities;
using Restraurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.IRepos
{
    public interface IRestaurantsRepo 
    {
        public Task<IEnumerable<Restaurant>> GetAllAsync();
        public Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string searchPhrase, int pageNumber, int pageSize, string sortBy ,SortDirection sortDirection);
        public Task<Restaurant> GetByIdAsync(int id);
        public Task<int> Create(Restaurant restaurant);
        public Task Delete(Restaurant restaurant);
        public Task SaveChanges();
    }
}
