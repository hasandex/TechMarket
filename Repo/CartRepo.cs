

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
                    var userCart = GetCartByUserId(_userId);//test if the user does not have a cart, then create a new cart for him
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
                        ProductId = productId,
                        CartId = userCart.Id,
                        ProductPrice = product.Price,
                    };
                    _appDbContext.CartContents.Add(cartContent);
                    _appDbContext.SaveChanges();
                    IncreaseTotalQuantityAndPrice();
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
        public Cart CreateCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("You must be logged in to perform this action");
            }
            var newCart = new Cart()
            {
                UserId = userId,
                CartContent = new List<CartContent>()   
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
                cart = CreateCart(userId);
            }
            return cart;
        }

        public int RemoveFromCart(int productId)
        {
            var cart = GetCartByUserId(_userId);
            if(cart != null)
            {
                var cartContent = cart.CartContent?.FirstOrDefault(p => p.ProductId == productId);
                if (cartContent != null)
                {
                    cart.TotalPrice -= cartContent.Product.Price * cartContent.Quantity;
                    cart.TotalProducts -= cartContent.Quantity;
                    _appDbContext.CartContents.Remove(cartContent);
                    return _appDbContext.SaveChanges();
                }
                return -1;
            }
            return -1;
        }

        public int EmptyCart(string userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var cartContet = cart.CartContent?.Where(cc => cc.CartId == cart.Id).ToList();
                _appDbContext.CartContents.RemoveRange(cartContet);
                cart.TotalPrice = 0;
                cart.TotalProducts = 0;
                return _appDbContext.SaveChanges();
            }
            return -1;
        }

        public int UpdateCartContentQuantity(int prodyctId,int quantity)
        {
            var cart = GetCartByUserId(_userId);
            if (cart != null)
            {
                var cartContent = cart.CartContent.Where(cc => cc.ProductId == prodyctId).SingleOrDefault();
                cartContent.Quantity = quantity;
                _appDbContext.SaveChanges();
                IncreaseTotalQuantityAndPrice();
                return 1;
            }
            return -1;
        }
        public int IncreaseTotalQuantityAndPrice()
        {
            var cart = GetCartByUserId(_userId);
            cart.TotalPrice = cart.CartContent.Sum(p => p.Product.Price * p.Quantity);
            cart.TotalProducts = cart.CartContent.Sum(p => p.Quantity);
            _appDbContext.SaveChanges();
            return 1;
        }
    }
}
