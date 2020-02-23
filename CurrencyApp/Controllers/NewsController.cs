using Microsoft.AspNetCore.Mvc;

namespace CurrencyApp.Controllers
{
    public class NewsController : Controller
    {
        public NewsController()
        {
        }

        public IActionResult TakeNews()
        {
            return View();
        }
    }
}
