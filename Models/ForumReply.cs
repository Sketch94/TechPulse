using System;

namespace TechPulse.Models
{
    public class ForumReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ThreadId { get; set; }
        public ForumThread Thread { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
