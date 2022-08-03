using Microsoft.AspNetCore.Mvc;

namespace DarEfta.Portal.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
