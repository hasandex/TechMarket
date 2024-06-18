using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.ViewModel
{
    public class CheckOutViewModel
    {
        public string UserId { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }

        public List<int> ProductId = new List<int>();
        public DateTime OrderCreatedTime { get; set; } = DateTime.Now;
        public int TotalProducts { get; set; }
        public double OrderPrice { get; set; }
        public string OrderStatus { get; set; } = "Sent";
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string Email { get; set; }
    }
}
