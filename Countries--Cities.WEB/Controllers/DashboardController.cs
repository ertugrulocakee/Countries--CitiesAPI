using Microsoft.AspNetCore.Mvc;

namespace Countries__Cities.WEB.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
