using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.Dtos
{
    public class RestaurantEditVm
    {
        public Guid Id { get; set; }

        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CuisineType CuisineType { get; set; }
        public int CuisineTypeId { get; set; }
        public List<CuisineType> AllCuisineTypes { get; set; }

        // set minimum date
        public DateTime Since { get; set; }
        public bool CoffeeShop { get; set; }
    }
}