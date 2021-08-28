using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
