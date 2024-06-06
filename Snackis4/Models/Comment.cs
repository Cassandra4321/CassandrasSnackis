﻿using Snackis4.Areas.Identity.Data;

namespace Snackis4.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public Snackis4User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public bool IsFlagged { get; set; }
    }
}
