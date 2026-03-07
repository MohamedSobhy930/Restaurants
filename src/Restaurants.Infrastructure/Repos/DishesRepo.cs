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
    public class DishesRepo : IDishesRepo
    {
        private readonly AppDbContext _context;
        public DishesRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Dish entity)
        {
            _context.Dishes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(IEnumerable<Dish> entities)
        {
            _context.Dishes.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
