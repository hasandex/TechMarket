namespace TechMarket.Services.IServices
{
    public interface IImageService
    {
        string StoreImage(IFormFile formFile, string itemImageStorePath);

        public interface IImageService
        {
            string StoreImage(IFormFile formFile, string path);
        }
    }
}
