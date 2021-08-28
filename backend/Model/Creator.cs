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
    }
}
