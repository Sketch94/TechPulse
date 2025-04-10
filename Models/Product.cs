using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechPulse.Models
{
    public class Product
    {
        public int Id { get; set; } // Unique identifier for the product

        public string? Username { get; set; } // Username of the product

        public string? Description { get; set; } // Description of the product

        public decimal Price { get; set; } // Price of the product

        public string? ImageUrl { get; set; } // URL of the product image
    }
}
