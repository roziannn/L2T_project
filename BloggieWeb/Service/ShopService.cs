using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Service
{
    public interface IShopService
    {
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> GetProductByHandle(string urlHandle);

    }
    public class ShopService(BloggieDbContext dbContext) : IShopService
    {
        private readonly BloggieDbContext _context = dbContext;

        public async Task<List<ProductDto>> GetAll()
        {
            try
            {
                var products = await _context.Products
                .Where(n => n.IsActive == true)
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
                throw new Exception("Failed to fetch notifications.", ex);
            }
        }

        public async Task<ProductDto> GetProductByHandle(string urlHandle)
        {
            try
            {
                var product = await _context.Products
                    .Where(n => n.UrlHandle == urlHandle && n.IsActive)
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
                    .FirstOrDefaultAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch product details.", ex);
            }
        }
    }
}
