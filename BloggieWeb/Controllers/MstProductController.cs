using BloggieWeb.Models.Domain;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Mvc;
using BloggieWeb.Data;
using Microsoft.EntityFrameworkCore;
using BloggieWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BloggieWeb.Controllers
{
    [Route("MstProduct")]
    [Authorize(Roles = "Admin")]

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

        [HttpGet("product-{id}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Product data)
        {

            if (ModelState.IsValid)
            {
                var result = _productService.Update(data);

                if (result)
                {
                    return Ok(new { message = "Product updated successfully" });
                }

                return BadRequest(new { message = "Failed to update product" });
            }

            return BadRequest(ModelState);
        }

        [HttpPut("Archieve")]
        public async Task<IActionResult> archieve([FromBody] Product data)
        {

            if (ModelState.IsValid)
            {
                var result = _productService.Archieve(data);

                if (result)
                {
                    return Ok(new { message = "Product updated successfully" });
                }

                return BadRequest(new { message = "Failed to update product" });
            }

            return BadRequest(ModelState);
        }

    }
}
