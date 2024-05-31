using TechMarket.Data;

namespace TechMarket.Repo
{
    public class RatingRepo : IRatingRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;
        public RatingRepo(AppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        public double GetProductRate(int productId)
        {
            var rate = _appDbContext.Ratings?.Where(r => r.ProductId == productId).FirstOrDefault();
            if(rate != null)
            {
                var productRate = _appDbContext.Ratings.Where(r => r.ProductId == productId).Average(r => r.RatingValue);
                return productRate;
            }
            return -1;
        }

        public int GetUserRate(string userId, int productId)
        {
            var rate = _appDbContext.Ratings?.Where(r=> r.UserId == userId && r.ProductId == productId).SingleOrDefault();
            if (rate == null)
            {
                return -1;
            }
            return rate.RatingValue;
        }

        public int RateProduct(int rate, int productId, string userId)
        {
            var rated = _appDbContext.Ratings?.Where(r => r.UserId == userId && r.ProductId == productId ).SingleOrDefault();
            if (rated != null)
            {
                UpdateRateProduct(rate, productId, userId);
                return 1;
            }
            var rating = new Rating()
            {
                RatingValue = rate,
                UserId = userId,
                ProductId = productId
            };
            _appDbContext.Ratings.Add(rating);
            return _appDbContext.SaveChanges();
        }
        public int UpdateRateProduct(int rate, int productId, string userId)
        {
            var rating = _appDbContext.Ratings
                .Where(r => r.UserId == userId && r.ProductId == productId)
                .SingleOrDefault();
            if (rating == null)
            {
                return 0;
            }
            rating.RatingValue = rate;
            _appDbContext.Ratings.Update(rating);
            return _appDbContext.SaveChanges();
        }
    }
}
