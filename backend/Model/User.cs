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
    }
}
