using System;

namespace Restaurant.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CuisineType CuisineType { get; set; }
        public int CuisineTypeId { get; set; }
        public DateTime Since { get; set; }
        public bool CoffeeShop { get; set; }
    }
}
