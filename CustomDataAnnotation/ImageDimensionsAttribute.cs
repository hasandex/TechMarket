using System.Drawing;

namespace TechMarket.CustomDataAnnotation
{
    public class ImageDimensionsAttribute : ValidationAttribute
    {
        private readonly int _width;
        private readonly int _height;
        public ImageDimensionsAttribute(int width, int height)
        {
           _width = width;
           _height = height;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                try
                {
                    var image = Image.FromStream(file.OpenReadStream());
                    // Do something with the image
                    int width = image.Width;
                    int height = image.Height;
                    if (width < 300 || height < 300)
                    {
                        return new ValidationResult(errorMessage: $"the image dimensions must be more than {Settings.ImageWidth} for Width and {Settings.ImageHeight} for Height");
                    }
                }
                catch (ArgumentException)
                {
                    // The file is not a valid image
                    return new ValidationResult(errorMessage: "The file is not a valid image.");
                }
            }
            return ValidationResult.Success;
            
        }
    }
}
