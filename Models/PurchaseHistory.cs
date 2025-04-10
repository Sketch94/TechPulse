namespace TechPulse.Models
{
    public class PurchaseHistory
    {
        public int Id { get; set; } // Unique identifier for the purchase history
        public int UserId { get; set; } // Foreign key to the user who made the purchase
        public User? User { get; set; } // Navigation property to the associated user
        public int ProductId { get; set; } // Foreign key to the purchased product
        public Product? Product { get; set; } // Navigation property to the associated product
        public DateTime PurchaseDate { get; set; } // Date and time of the purchase
        public decimal TotalAmount { get; set; } // Total amount of the purchase
    }
}