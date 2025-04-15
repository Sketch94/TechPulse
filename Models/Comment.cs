using System;

namespace TechPulse.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int? ProductId { get; set; } 
        public Product Product { get; set; }
    }
}
