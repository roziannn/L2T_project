using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Service
{

    public interface IMstProductService
    {
        Task<Product> Create(Product product);
        Task<List<Product>> GetAll();


    }

    public class MstProductService(BloggieDbContext dbContext) : IMstProductService
    {
        private readonly BloggieDbContext _context = dbContext;

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
            };

            _context.Products.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<List<Product>> GetAll()
        {
            try
            {
                var products = await _context.Products
                .Where(n => n.IsActive == true)
                .Select(n => new Product
                {
                    Id = n.Id,
                    ProductName = n.ProductName,
                    ProductCategory = n.ProductCategory,
                    Description = n.Description,
                    NormalPrice = n.NormalPrice,
                    Discount = n.Discount,
                    Stock = n.Stock,
                    IsActive = n.IsActive,
                    CreatedAt = n.CreatedAt,
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
    }
}
