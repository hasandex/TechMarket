using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Range(0,int.MaxValue,ErrorMessage = "Please enter a positive number")]
        public double Price { get; set; } = 0;
        public string Cover { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
