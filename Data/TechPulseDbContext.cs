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
        public DbSet<Article> Articles { get; set; } // Table for storing articles

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Entity<PurchaseHistory>()
                .Property(ph => ph.TotalAmount)
                .HasPrecision(18, 2);

            // Seed example Articles with static dates
            builder.Entity<Article>().HasData(
                new Article
                {
                    Id = 1,
                    Title = "Hur du blir produktiv som utvecklare",
                    Content = "Det handlar om vanor, fokus och att använda rätt verktyg.",
                    AuthorId = "TechPulse Team",
                    CreatedAt = new DateTime(2024, 01, 10, 8, 0, 0, DateTimeKind.Utc),
                    IsPublished = true,
                    Category = "Produktivitet"
                },
                new Article
                {
                    Id = 2,
                    Title = "Varför Tailwind CSS är bättre än du tror",
                    Content = "Utility-first CSS låter dig bygga snabbt och konsekvent.",
                    AuthorId = "TechPulse Team",
                    CreatedAt = new DateTime(2024, 01, 08, 12, 0, 0, DateTimeKind.Utc),
                    IsPublished = true,
                    Category = "Frontend"
                }
            );
        }
    }
}
