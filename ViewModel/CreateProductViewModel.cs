using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechMarket.ViewModel
{
    public class CreateProductViewModel
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive number")]
        public double Price { get; set; } = 0;
        [AllowedExtension(Settings.AllowedImgExtensions)]
        [MaxFileSize(Settings.MaxImgSize)]
        public IFormFile FormFile { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoriesSelectList { get; set; } = new List<SelectListItem>();
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive number")]
        public int Quantity { get; set; }

    }
}
