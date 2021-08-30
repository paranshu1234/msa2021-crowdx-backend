using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string? Content { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public int UserId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
