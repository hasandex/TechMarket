namespace TechMarket.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public int TotalProducts { get; set; } = 1;
        public double TotalPrice { get; set; } = 0;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public ICollection<CartContent>? CartContent { get; set; }
    }
}
