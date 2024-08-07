﻿
using Microsoft.AspNetCore.Hosting;
using TechMarket.Migrations;

namespace TechMarket.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;
        public ProductRepo(AppDbContext appDbContext,
            IWebHostEnvironment env, IImageService imageService,IUserService userService)
        {
            _appDbContext = appDbContext;
            _env = env;
            _imageService = imageService;
            _userService = userService;
        }

 
        //this method for Admin Index
        public int ChangeProductState(int id)
        {
            var product = GetById(id);
            if (product == null)
            {
                return -1;
            }
            product.Status = "To Be Determined";
            product.AdminMessage = null;
            return _appDbContext.SaveChanges();
        }
        public int DoSome(string action, string? message, int productId)
        {
            var product = GetById(productId);
            if (product == null)
            {
                return -1;
            }
            if(action == "Accept")
            {
                product.Status = "Accepted";
            }
            else if(action == "Reject")
            {
                product.Status = "Rejected";
            }
            product.AdminMessage = message;
            return _appDbContext.SaveChanges();
        }


        //this method for Admin Index
        public async Task<int> GetCountAllNewProducts()
        {
            return await _appDbContext.Products.Where(p => p.Status == "To Be Determined")
               .AsNoTracking()
               .CountAsync();
        }
        public async Task<int> GetCountAllProducts()
        {
            return await _appDbContext.Products.AsNoTracking().CountAsync();
        }
        //this method for Home Index
        public async Task<IEnumerable<Product>> GetAllAvailable()
        {
            return await _appDbContext.Products.Include(p => p.Category)
                .Where(p=>p.Status == "Accepted")
                .AsNoTracking()
                .ToListAsync();
        }
        //this method for Admin Index
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _appDbContext.Products.Include(p => p.Category)
                .Include(p=>p.User)                               
                .AsNoTracking().ToListAsync();
        }
        //this method for User Index
        public async Task<IEnumerable<Product>> GetAll(string userId)
        {
            return await _appDbContext.Products.Include(p => p.Category)
                .Where(p => p.UserId == userId)
                .AsNoTracking().ToListAsync();
        }
        public Product? GetById(int id)
        {
            var product = _appDbContext.Products.Include(p=>p.Category)
                .Include(p=>p.Ratings)
                .Include(p => p.Comments)
                .ThenInclude(c=>c.User)
                .FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return product;
            }
            return null;
        }
        public int Create(CreateProductViewModel viewModel)
        {
            var product = new Product()
            {
                UserId = _userService.GetUserId(),
                Name = viewModel.Name,
                Description = viewModel.Description,
                CategoryId = viewModel.CategoryId,
                Price = viewModel.Price,
                Cover = _imageService.StoreImage(viewModel.FormFile, Settings.ItemImageStorePath),
                Quantity = viewModel.Quantity,
            };
            _appDbContext.Products.Add(product);
            return _appDbContext.SaveChanges();
        }

        public int Update(UpdateProductViewModel viewModel)
        {
            var item = _appDbContext.Products.FirstOrDefault(i => i.Id == viewModel.Id);
            if (item != null)
            {
                if (viewModel.FormFile != null)
                {
                    var oldImagePath = Path.Combine($"{_env.WebRootPath}{Settings.ItemImageStorePath}", item.Cover);
                    File.Delete(oldImagePath);
                    item.Cover = _imageService.StoreImage(viewModel.FormFile, Settings.ItemImageStorePath);
                }
                item.Name = viewModel.Name;
                item.Description = viewModel.Description;
                item.CategoryId = viewModel.CategoryId;
                item.Price = viewModel.Price;
                item.Quantity = viewModel.Quantity;
                _appDbContext.Update(item);
                return _appDbContext.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            var product = _appDbContext.Products.FirstOrDefault(i => i.Id == id);

            if (product != null)
            {
                _appDbContext.Products.Remove(product);
                var productImage = Path.Combine($"{_env.WebRootPath}{Settings.ItemImageStorePath}", product.Cover);
                File.Delete(productImage);
                return _appDbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

    }
}
