using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FName {  get; set; }
        public string LName { get; set; }
        [NotMapped]
        public IFormFile? ProfilePictureFile { get; set; }
        public byte[]? ProfilePicture { get; set; } 
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
