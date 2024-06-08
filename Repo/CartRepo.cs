namespace TechMarket.Repo
{
    public class CartRepo : ICartRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;
        private readonly string _userId;
        public CartRepo(AppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
            _userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(_userId))
            {
                throw new UnauthorizedAccessException("You must be logged in to perform this action");
            }
        }
        public int AddToCart(int productId)
        {
            using (var transaction = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    var userCart = GetCartByUserId(_userId);
                    if (userCart == null)
                    {
                        userCart = CreateCart(_userId);
                    }
                    var cartContent = userCart.CartContent?.FirstOrDefault(c => c.ProductId == productId);
                    if (cartContent != null)
                    {
                        return -1;
                    }
                    var product = _appDbContext.Products.FirstOrDefault(p => p.Id == productId);
                    if (product == null)
                    {
                        throw new ArgumentException($"Item with ID {productId} not found");
                    }
                    cartContent = new CartContent
                    {
                        // ItemId = itemId,
                        ProductId = productId,
                        CartId = userCart.Id,
                        ProductPrice = product.Price
                    };
                    _appDbContext.CartContents.Add(cartContent);
                    userCart.TotalPrice = userCart.CartContent.Sum(c => c.ProductPrice);
                    userCart.TotalProducts = userCart.CartContent.Count(c => c.CartId == userCart.Id);
                    _appDbContext.SaveChanges();
                    transaction.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public int Count(string userId)
        {
            throw new NotImplementedException();
        }

        public Cart CreateCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("You must be logged in to perform this action");
            }
            var newCart = new Cart()
            {
                UserId = userId,
            };
            _appDbContext.Carts.AddAsync(newCart);
            _appDbContext.SaveChanges();
            return newCart;
        }

        public Cart GetCartByUserId(string userId)
        {
            var cart = _appDbContext.Carts?.Include(c => c.CartContent)
                .ThenInclude(c => c.Product)
                .ThenInclude(i => i.Category)
                .FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                return null;
            }
            return cart;
        }

        public CartContent GetCartContent(int productId)
        {
            throw new NotImplementedException();
        }

        public int RemoveFromCart(int productId)
        {
            throw new NotImplementedException();
        }

        public double TotalPrice(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
