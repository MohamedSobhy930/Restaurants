using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.IRepos
{
    public interface IDishesRepo 
    {
        Task<int> Create(Dish entity);
        Task Delete(IEnumerable<Dish> entities);
    }
}
