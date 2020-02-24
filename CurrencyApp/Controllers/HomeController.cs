using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CurrencyApp.Models;
using System.Net;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using Json.Net;

namespace CurrencyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            WebRequest request = WebRequest.Create("https://api.exchangeratesapi.io/history?start_at=2020-02-20&end_at=2020-02-25&base=USD&symbols=RUB,EUR");
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string a = (reader.ReadToEnd());
                    //string data = "{\"2020-02-24\": { \"EUR\":0.9243852838,\"RUB\":65.323997042},\"2020-02-21\":{ \"EUR\":0.9258402,\"RUB\":64.3903342283}}";
                    analyzeResponseData(a);
                }
            }
            response.Close();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static void analyzeResponseData(string jsonData)
        {
            
        }
        /*
         * 
        {"rates":
            {"2020-02-24":
                {"EUR":0.9243852838,"RUB":65.323997042},
             "2020-02-21":
                {"EUR":0.9258402,"RUB":64.3903342283},
             "2020-02-20":{"EUR":0.9267840593,"RUB":63.7683039852}
             },
         "start_at":"2020-02-20","base":"USD","end_at":"2020-02-25"}

         */
    }
}
