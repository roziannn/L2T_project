using BloggieWeb.Data;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    [Route("shop")]

    public class ShopController(IShopService shopService, BloggieDbContext dbContext) : Controller
    {
        private readonly IShopService _shopService = shopService;
        private readonly BloggieDbContext _context = dbContext;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _shopService.GetAll();
            return Ok(products);
        }

        [HttpGet("product/learn2tech-{urlHandle}")]
        public async Task<IActionResult> Show(string urlHandle)
        {
            var product = await _shopService.GetProductByHandle(urlHandle);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
