
namespace TechMarket.Repo.IRepo
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAll();
        Product? GetById(int id);
        int Create(CreateProductViewModel viewModel);
        int Update(UpdateProductViewModel viewModel);
        int Delete(int id);
    }
}
