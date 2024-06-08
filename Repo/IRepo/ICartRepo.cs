namespace TechMarket.Repo.IRepo
{
    public interface ICartRepo
    {
        Cart CreateCart(string userId);
        Cart GetCartByUserId(string userId);
        CartContent GetCartContent(int productId);
        int AddToCart(int productId);
        int RemoveFromCart(int productId);
        double TotalPrice(string userId);
        int Count(string userId);
    }
}
