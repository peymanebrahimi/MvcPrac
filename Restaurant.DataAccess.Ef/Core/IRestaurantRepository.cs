using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Ef.Core
{
    public interface IRestaurantRepository : IRepository<Models.Restaurant>
    {
        Task<IEnumerable<Models.Restaurant>> Search(string userName);
    }
}