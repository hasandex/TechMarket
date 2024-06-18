using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
