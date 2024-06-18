using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public DateTime OrderCreatedTime { get; set; } = DateTime.Now;
        public string OrderStatus { get; set; } = "Sent";
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string Email {  get; set; }
        public Cart? Cart { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
