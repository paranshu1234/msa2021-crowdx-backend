using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; } = null!;

        [Required]
        public int CreatorId { get; set; }

        public Creator Creator { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
