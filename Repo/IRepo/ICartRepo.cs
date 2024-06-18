namespace TechMarket.Repo.IRepo
{
    public interface ICartRepo
    {
        Cart CreateCart(string userId);
        Cart GetCartByUserId(string userId);
        int AddToCart(int productId);
        int RemoveFromCart(int productId);
        int EmptyCart(string userId);
        int UpdateCartContentQuantity(int productId,int quantity);
      //  double TotalPrice(string userId);
      // int Count(string userId);
      // UserCartViewModel UserCartContent(string userId);
    }
}
