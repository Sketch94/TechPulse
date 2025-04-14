using System;
using System.ComponentModel.DataAnnotations;

namespace TechPulse.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public required string Title { get; set; } = string.Empty;

        [Required]
        public required string Content { get; set; }

        public string? AuthorId { get; set; }  // Can be nullable for unauthenticated creation

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsPublished { get; set; }

        public string? FeaturedImage { get; set; }

        [Required]
        public required string Category { get; set; }

        // REMOVE this 👇
        // public virtual User? Author { get; set; }  ← ❌ You don't have a User model
    }
}
