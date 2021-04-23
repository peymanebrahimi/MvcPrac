using System.ComponentModel.DataAnnotations;
using Restaurant.Models;

namespace ScottFundamentals.Models
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}