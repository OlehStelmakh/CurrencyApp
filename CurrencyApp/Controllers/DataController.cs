using System;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApp.Controllers
{
    public class DataController : Controller
    {
        public IActionResult TakeData()
        {
            return View();
        }
    }
}
