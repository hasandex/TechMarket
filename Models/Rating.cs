using System.CodeDom;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int RatingValue { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
