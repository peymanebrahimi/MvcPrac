using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.DataAccess.Ef.Infrastructure
{
    public class RestaurantDbContext : DbContext
    {

        public RestaurantDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Models.Restaurant> Restaurants { get; set; }
        public DbSet<CuisineType> CuisineTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuisineType>().HasData(
                new List<CuisineType>()
                {
                    new CuisineType(){Id = 1,Name = "Italian"},
                    new CuisineType(){Id = 2,Name = "Mexican"},
                    new CuisineType(){Id = 3,Name = "American"},
                }
            );
        }
    }



}
