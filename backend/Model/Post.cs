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
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Likes { get; set; }

        public string ImageURI { get; set; }
        [Required]
        public int CreatorId { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
