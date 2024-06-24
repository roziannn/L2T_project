using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    [Route("Dashboard")]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
