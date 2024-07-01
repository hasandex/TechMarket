
namespace TechMarket.Repo.IRepo
{
    public interface IProductRepo
    {
        Task<int> GetCountAllNewProducts();
        Task<int> GetCountAllProducts();
        Task<IEnumerable<Product>> GetAllAvailable();
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetAll(string userId);
        Product? GetById(int id);
        int Create(CreateProductViewModel viewModel);
        int Update(UpdateProductViewModel viewModel);
        int Delete(int id);
        int ChangeProductState(int id);
        int DoSome(string action,string? message, int productId);
    }
}
