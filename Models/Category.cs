

using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace TechMarket.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
