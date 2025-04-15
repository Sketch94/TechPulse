using System;
using System.Collections.Generic;

namespace TechPulse.Models
{
    public class ForumThread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<ForumReply> Replies { get; set; }
    }
}
