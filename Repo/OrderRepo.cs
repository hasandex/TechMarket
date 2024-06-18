
using TechMarket.Data;

namespace TechMarket.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICartRepo _cartRepo;
        private readonly IUserService _userService;
        private readonly string _userId;
        public OrderRepo(AppDbContext appDbContext, ICartRepo cartRepo, IUserService userService)
        {
            _appDbContext = appDbContext;
            _cartRepo = cartRepo;
            _userService = userService;
            _userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(_userId))
            {
                throw new UnauthorizedAccessException("You must be logged in to perform this action");
            }
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
