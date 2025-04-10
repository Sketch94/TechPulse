using Microsoft.EntityFrameworkCore;
using TechPulse.Models;

namespace TechPulse.Data
{
    public class TechPulseDbContext : DbContext
    {
        public TechPulseDbContext(DbContextOptions<TechPulseDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } // Table for storing user information
        public DbSet<Product> Products { get; set; } // Table for storing product information
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; } // Table for storing purchase history information

    }
}
