using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Creator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CreatorName { get; set; }

        public string CoverImageURI { get; set; }

        public string AvatarImageURI { get; set; }

        public ICollection<Post> Posts { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
