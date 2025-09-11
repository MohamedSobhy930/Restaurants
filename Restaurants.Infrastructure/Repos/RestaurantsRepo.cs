using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Restaurants.Domain.IRepos;
using Restaurants.Infrastructure.Persistence;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
