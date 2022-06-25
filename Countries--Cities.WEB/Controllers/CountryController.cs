using Microsoft.AspNetCore.Mvc;

namespace Countries__Cities.WEB.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
