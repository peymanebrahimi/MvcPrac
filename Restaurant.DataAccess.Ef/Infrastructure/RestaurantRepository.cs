using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Ef.Core;

namespace Restaurant.DataAccess.Ef.Infrastructure
{
    public class RestaurantRepository : Repository<Models.Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RestaurantDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Models.Restaurant>> Search(string userName)
        {
            var orderList = await _dbContext.Restaurants
                //.Where(o => o.UserName == userName)
                .ToListAsync();

            return orderList;
        }
    }
}