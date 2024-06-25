using BloggieWeb.Models.Domain;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Mvc;
using BloggieWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Controllers
{
    [Route("MstProduct")]
    public class MstProductController(IMstProductService productService, BloggieDbContext dbContext) : Controller
    {

        private readonly IMstProductService _productService = productService;
        private readonly BloggieDbContext _context = dbContext;


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }

            var data = await _productService.Create(product);
            return Ok(data);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }
    }
}
