using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.Dtos
{
    public class RestaurantSearchFormModel
    {
        public List<CuisineType> AllCuisineTypes { get; set; }

        [MaxLength(80)]
        public string Name { get; set; }

        public CuisineType CuisineType { get; set; }
        public int CuisineTypeId { get; set; }

        public DateTime Since { get; set; }
        public bool CoffeeShop { get; set; }

        public IEnumerable<Result> Results { get; set; }
        
    }

    public class Result
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CuisineType CuisineType { get; set; }
        public DateTime Since { get; set; }
        public bool CoffeeShop { get; set; }
    }
}