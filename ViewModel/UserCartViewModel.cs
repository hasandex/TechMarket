namespace TechMarket.ViewModel
{
    public class UserCartViewModel
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public int TotalProducts { get; set; } = 1;
        public double TotalPrice { get; set; } = 0;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public List<Product>? Products { get; set;}
        public int? Quantity { get; set; } = 1;
    }
}
