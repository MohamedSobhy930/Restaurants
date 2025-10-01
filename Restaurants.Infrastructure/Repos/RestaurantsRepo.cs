using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Restaurants.Domain.IRepos;
using Restaurants.Infrastructure.Persistence;
using Restraurants.Domain.Entities;
using Restraurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repos
{
    public class RestaurantsRepo : IRestaurantsRepo
    {
        private readonly AppDbContext _context;
        public RestaurantsRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants =await _context.Restaurants
                .Include(r => r.Dishes)
                .ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id) 
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id); 
            return restaurant;
        }
        public async Task<int> Create(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task Delete(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(
            string searchPhrase, int pageNumber, int pageSize, string sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var query = _context.Restaurants
                .Where(r => searchPhraseLower == null ||
                (r.Name.ToLower().Contains(searchPhraseLower) || r.Description.ToLower().Contains(searchPhraseLower)));
            var totalCount = query.Count();

            if(sortBy != null)
            {
                var columnselector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), r => r.Name },
                    {nameof(Restaurant.Category), r => r.Category },
                    {nameof(Restaurant.Description), r => r.Description }
                };
                query = sortDirection == SortDirection.Ascending ?
                    query.OrderBy(columnselector[sortBy]) :
                    query.OrderByDescending(columnselector[sortBy]);
            }

            var restaurants = await query
                .Skip(pageSize * (pageNumber-1))
                .Take(pageSize)
                .Include(r => r.Dishes)
                .ToListAsync();
            return (restaurants,totalCount);
        }
    }
}
