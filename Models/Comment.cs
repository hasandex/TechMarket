using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string Content { get; set; }
        public Product? Product { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
