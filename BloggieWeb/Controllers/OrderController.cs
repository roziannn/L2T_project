using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    [Route("Orders")]

    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Detail")]
        public IActionResult Detail()
        {
            return View();
        }
    }
}
