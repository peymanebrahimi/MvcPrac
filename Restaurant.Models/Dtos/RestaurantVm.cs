using System;

namespace Restaurant.Models.Dtos
{
    public class RestaurantVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public CuisineType CuisineType { get; set; }
        public DateTime Since { get; set; }
        public bool CoffeeShop { get; set; }
    }
}