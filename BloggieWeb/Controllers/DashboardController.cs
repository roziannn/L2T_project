using BloggieWeb.Data;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BloggieWeb.Controllers
{
    [Route("Dashboard")]

    public class DashboardController(IMstProductService mstProductService, IMstUserService mstUserService,
        BloggieDbContext dbContext, AuthDbContext authContext) : Controller
    {
        private readonly IMstProductService _mstProductService = mstProductService;
        private readonly IMstUserService _mstUserService = mstUserService;
        private readonly BloggieDbContext _context = dbContext;
        private readonly AuthDbContext _authContext = authContext;

        public async Task<IActionResult> Index()
        {
            try
            {
                // Fetch product data
                var products2 = await _mstProductService.GetAll();

                var productData2 = products2
                    .GroupBy(p => p.IsActive ? "Active" : "Inactive")
                    .Select(g => new
                    {
                        Category = g.Key,
                        Data = g.Select(p => new { p.ProductName, p.Stock }).ToList()
                    })
                    .ToList();

                var seriesData2 = productData2.Select(pd => new
                {
                    name = pd.Category,
                    data = pd.Data.Select(d => d.Stock).ToList()
                })
                .ToList();

                ViewBag.SeriesData2 = JsonConvert.SerializeObject(seriesData2);


                // Fetch product data
                var products = await _mstProductService.GetAll();

                var productData = products
                    .GroupBy(p => p.ProductCategory)
                    .Select(g => new
                    {
                        Category = g.Key,
                        Data = g.Select(p => new { p.ProductName, p.Stock }).ToList()
                    })
                    .ToList();

                var seriesData = productData.Select(pd => new
                {
                    name = pd.Category,
                    data = pd.Data.Select(d => d.Stock).ToList()
                })
                .ToList();

                ViewBag.SeriesData = JsonConvert.SerializeObject(seriesData);


                return View();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


    }

}
