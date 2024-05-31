namespace TechMarket.Repo.IRepo
{
    public interface IRatingRepo
    {
        int RateProduct(int rate, int productId, string userId);
        int UpdateRateProduct(int rate, int productId, string userId);
        int GetUserRate(string userId, int productId);
        double GetProductRate(int productId);
    }
}
