using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Service
{

    public interface IMstProductService
    {
        Task<Product> Create(Product product);
        Task<Product> GetById(Guid id);
        bool Update(Product model);
        bool Archieve(Product model);
        Task<List<ProductDto>> GetAll();
    }

    public class MstProductService(BloggieDbContext dbContext) : IMstProductService
    {
        private readonly BloggieDbContext _context = dbContext;

        public bool Archieve(Product model)
        {
            var product = _context.Products.Find(model.Id);

            if (product == null)
            {
                return false;
            }

            product.IsActive = false;

            _context.Products.Update(product);
            _context.SaveChanges();

            return true;
        }

        public async Task<Product> Create(Product p)
        {
            var data = new Product
            {
                Id = Guid.NewGuid(), 
                ProductName = p.ProductName,
                ProductCategory = p.ProductCategory,
                Description = p.Description,
                NormalPrice = p.NormalPrice,
                Discount = p.Discount,
                Stock = p.Stock,
                CreatedBy = "Admin", 
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true,
                ImageUrl = p.ImageUrl,
                UrlHandle = p.UrlHandle,
            };

            _context.Products.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            try
            {
                var products = await _context.Products
                //.Where(n => n.IsActive == true)
                .Select(n => new ProductDto
                {
                    Id = n.Id,
                    ProductName = n.ProductName,
                    ProductCategory = n.ProductCategory,
                    Description = n.Description,
                    NormalPrice = n.NormalPrice,
                    Discount = n.Discount,
                    DiscountedPrice = n.DiscountedPrice,
                    Stock = n.Stock,
                    IsActive = n.IsActive,
                    CreatedAt = n.CreatedAt,
                    ImageUrl = n.ImageUrl,
                    UrlHandle = n.UrlHandle,
                })
                .ToListAsync();

                
                return products;
            }
            catch (Exception ex)
            {
                // Di sini Anda bisa menangani exception, misalnya log ke sistem atau kirimkan notifikasi kesalahan
                throw new Exception("Failed to fetch notifications.", ex);
            }
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

       
        public bool Update(Product model)
        {
            var product = _context.Products.Find(model.Id);

            if (product == null)
            {
                return false;
            }

            product.ProductName = model.ProductName;
            product.ProductCategory = model.ProductCategory;
            product.Description = model.Description;
            product.NormalPrice = model.NormalPrice;
            product.Discount = model.Discount;
            product.Stock = model.Stock;
            product.ImageUrl = model.ImageUrl;
            product.UrlHandle = model.UrlHandle;

            _context.Products.Update(product);
            _context.SaveChanges();

            return true;
        }

    }
}
