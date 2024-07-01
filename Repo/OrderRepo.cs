
using TechMarket.Data;

namespace TechMarket.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _appDbContext;
        public readonly IProductRepo _productRepo;
        private readonly ICartRepo _cartRepo;
        private readonly IUserService _userService;
        private readonly string _userId;
        public OrderRepo(AppDbContext appDbContext, ICartRepo cartRepo, IUserService userService, IProductRepo productRepo)
        {
            _appDbContext = appDbContext;
            _cartRepo = cartRepo;
            _userService = userService;
            _userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(_userId))
            {
             
                throw new UnauthorizedAccessException("You must be logged in to perform this action");
            }
            _productRepo = productRepo;
        }
        //this method for Admin
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _appDbContext.Orders.Include(o=>o.Cart)
                .Include(o=>o.OrderProducts)
                .OrderByDescending(o=>o.OrderCreatedTime)
                .AsNoTracking().ToListAsync();
        }
        //this method for User
        public async Task<IEnumerable<Order>> GetOrders(string userId)
        {
            return await _appDbContext.Orders.Include(o => o.Cart)
                .Include(o => o.OrderProducts)
                .Where(o=>o.UserId == userId)
                .AsNoTracking().ToListAsync();
        }
        public async Task<Order> GetOrderById(int orderId)
        {
            var order = await _appDbContext.Orders.Include(o => o.Cart)
                .Include(o => o.OrderProducts)
                .ThenInclude(op=>op.Product)
                .Where(o => o.Id == orderId)
                .AsNoTracking().SingleOrDefaultAsync();
            if (order != null)
                return order;
            return null;
        }
        public int Add(Order order)
        {
            using (var transaction = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    var cart = _cartRepo.GetCartByUserId(order.UserId);
                    var cartContent = cart.CartContent;
                    _appDbContext.Orders.Add(order);              
                    order.OrderProducts = cartContent.Select(cc => new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = cc.ProductId,
                        Price = cc.ProductPrice,
                        Quantity = cc.Quantity,
                        TotalPrice = cc.ProductPrice * cc.Quantity
                        
                    }).ToList();
                    _cartRepo.EmptyCart(_userId);
                    foreach (var p in order.OrderProducts)
                    {
                        var product = _productRepo.GetById(p.ProductId);
                        product.Quantity -= p.Quantity;
                    }
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
    }
}
