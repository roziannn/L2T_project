using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            return View();
        }
    }
}
