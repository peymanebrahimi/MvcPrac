using System;

namespace ScottFundamentals.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CuisineType CuisineType { get; set; }
        public int CuisineId { get; set; }
        public DateTime Since { get; set; }
        
    }

    public class CuisineType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}