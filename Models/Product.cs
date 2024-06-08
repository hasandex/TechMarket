using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Range(1,int.MaxValue,ErrorMessage = "Please enter a positive number")]
        public double Price { get; set; } = 0;
        public string Cover { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string IsAvailable { get; set; } = "To Be Determined";
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<CartContent>? CartContent { get; set; }
    }
}
