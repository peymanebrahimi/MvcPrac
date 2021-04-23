using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Ef.Infrastructure;
using Restaurant.Models.Dtos;

namespace ScottFundamentals.ViewComponents
{
    public class SearchRestaurantViewComponent : ViewComponent
    {
        private readonly RestaurantDbContext _context;

        public SearchRestaurantViewComponent(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name, bool coffeeShop)
        {
            var items = await _context.Restaurants
                .Where(x => x.Name.Contains(name) && x.CoffeeShop == coffeeShop)
                .AsNoTracking()
                .Select(r => new RestaurantVm()
                {
                    CuisineType = r.CuisineType,
                    Id = r.Id,
                    Name = r.Name,
                    Since = r.Since,
                    CoffeeShop = r.CoffeeShop
                })
                .ToListAsync();

            return View(items);
        }
    }
}