using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Ef.Infrastructure;
using Restaurant.Models;
using Restaurant.Models.Dtos;

namespace ScottFundamentals.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly RestaurantDbContext _context;

        public RestaurantsController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var model = await (from r in _context.Restaurants.AsNoTracking()
                               select new RestaurantVm()
                               {
                                   CuisineType = r.CuisineType,
                                   Id = r.Id,
                                   Name = r.Name,
                                   Since = r.Since,
                                   CoffeeShop = r.CoffeeShop
                               }).ToListAsync();

            return View(model);
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.AsNoTracking()
                .Select(x => new RestaurantVm()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Since = x.Since,
                    CuisineType = x.CuisineType,
                    CoffeeShop = x.CoffeeShop
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public async Task<IActionResult> Create()
        {
            var allCuisine = await _context.CuisineTypes.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

            var model = new RestaurantEditVm() { AllCuisineTypes = allCuisine };

            return View(model);
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CuisineTypeId,Since,CoffeeShop")] RestaurantEditVm restaurantVm)
        {
            if (ModelState.IsValid)
            {
                var restaurant = new Restaurant.Models.Restaurant()
                {
                    Name = restaurantVm.Name,
                    CuisineTypeId = restaurantVm.CuisineTypeId,
                    //CuisineType = restaurantVm.CuisineType,
                    Since = restaurantVm.Since,
                    CoffeeShop = restaurantVm.CoffeeShop,
                    Id = Guid.NewGuid()
                };

                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurantVm);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var allCuisine = await _context.CuisineTypes.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

            var model = new RestaurantEditVm()
            {
                AllCuisineTypes = allCuisine,
                //CuisineType = restaurant.CuisineType,
                CuisineTypeId = restaurant.CuisineTypeId,
                Id = restaurant.Id,
                Name = restaurant.Name,
                Since = restaurant.Since,
                CoffeeShop = restaurant.CoffeeShop
            };

            return View(model);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CuisineTypeId,Since,CoffeeShop")] RestaurantEditVm restaurantVm)
        {
            if (id != restaurantVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var model = new Restaurant.Models.Restaurant()
                    {
                        CuisineTypeId = restaurantVm.CuisineTypeId,
                        Id = restaurantVm.Id,
                        Name = restaurantVm.Name,
                        Since = restaurantVm.Since,
                        CoffeeShop = restaurantVm.CoffeeShop
                    };

                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurantVm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurantVm);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.AsNoTracking()
                .Select(x => new RestaurantVm()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Since = x.Since,
                    CuisineType = x.CuisineType,
                    CoffeeShop = x.CoffeeShop
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(Guid id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var allCuisine = await _context.CuisineTypes.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

            var model = new RestaurantSearchFormModel()
            {
                AllCuisineTypes = allCuisine,
                Results = new List<Result>(),
                Since = new DateTime(2010)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(RestaurantSearchFormModel searchModel)
        {
            var q = _context.Restaurants.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                q = q.Where(x => x.Name.Contains(searchModel.Name));
            }

            if (searchModel.CoffeeShop)
            {
                q = q.Where(x => x.CoffeeShop == searchModel.CoffeeShop);
            }

            //if (searchModel.CuisineType?.Id != 0)
            //{
            //    q = q.Where(x => x.CuisineTypeId == searchModel.CuisineType.Id);
            //}

            if (searchModel.Since.Year > 1900)
            {
                q = q.Where(x => x.Since.Date == searchModel.Since.Date);
            }

            var items = await q
                .AsNoTracking()
                .Select(r => new Result()
                {
                    CuisineType = r.CuisineType,
                    Id = r.Id,
                    Name = r.Name,
                    Since = r.Since,
                    CoffeeShop = r.CoffeeShop
                })
                .ToListAsync();
            
            var allCuisine = await _context.CuisineTypes.AsNoTracking().OrderBy(x => x.Name).ToListAsync();
            searchModel.Results = items;
            searchModel.AllCuisineTypes = allCuisine;
            
            //return PartialView("_SearchResult", searchModel);
            return View(searchModel);
        }
    }
}
