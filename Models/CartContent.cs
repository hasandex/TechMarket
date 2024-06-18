using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.Models
{
    public class CartContent
    {
        public int Id { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public double ProductPrice { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive number")]
        public int Quantity { get; set; } = 1;
        public Cart? Cart { get; set; }
        public Product? Product { get; set; }
    }
}
