using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Likes { get; set; }

        public string ImageURI { get; set; }
    }
}
