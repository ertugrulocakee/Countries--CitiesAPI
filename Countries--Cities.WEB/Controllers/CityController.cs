using Microsoft.AspNetCore.Mvc;

namespace Countries__Cities.WEB.Controllers
{
    public class CityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
