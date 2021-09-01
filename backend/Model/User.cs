using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string ImageURI { get; set; }

        public Creator Creator { get; set; } = null!;

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
