using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public string Likes { get; set; } = null!;

        public string ImageURI { get; set; } = null!;
        [Required]
        public int CreatorId { get; set; }

        public Creator Creator { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
