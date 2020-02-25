using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CurrencyApp.Models;
using System.Net;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using Json.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            string request = "https://api.exchangeratesapi.io/history?start_at=2020-02-20&end_at=2020-02-25&base=USD&symbols=RUB,EUR";
            createResponse(request);
            createDataLists(Rates.Data);

            ViewBag.FirstCurrencyValues = Rates.currencies["EUR"].ToArray();
            ViewBag.SecondCurrencyValues = Rates.currencies["RUB"].ToArray();
            ViewBag.Dates = Rates.availableDates.ToArray();

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

        public static void createResponse(string requestMessage)
        {
            WebRequest request = WebRequest.Create(requestMessage);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    analyzeResponseData(reader.ReadToEnd());
                }
            }
            response.Close();
        }

        public static void analyzeResponseData(string jsonData)
        {
            Rates.Data = new Dictionary<DateTime, Dictionary<string, double>>();
            
            var jsonObject = JObject.Parse(jsonData);

            CurrencyData.startAt = getDateFromString(jsonObject["start_at"].ToString());
            CurrencyData.endAt = getDateFromString(jsonObject["end_at"].ToString());
            CurrencyData.baseCurrency = jsonObject["base"].ToString();

            var days = jsonObject["rates"];
            foreach (JProperty day in days) {

                DateTime dateTime = getDateFromString(day.Name);
                Dictionary<string, double> keyValues = new Dictionary<string, double>();
                foreach (JProperty currency in day.Value)
                {
                    keyValues.Add(currency.Name, double.Parse(currency.Value.ToString()));
                }
                Rates.Data.Add(dateTime, keyValues);
            }
        }

        public static DateTime getDateFromString(string date)
        {
            int year = int.Parse(date.Substring(0, 4));
            int month = int.Parse(date.Substring(5, 2));
            int day = int.Parse(date.Substring(8, 2));
            return new DateTime(year, month, day);
        }

        public static void createDataLists(Dictionary<DateTime, Dictionary<string, double>> data)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (var key in data)
            {
                dates.Add(key.Key);
            }
            dates.Reverse();
            Rates.availableDates = new List<DateTime>(dates);

            Dictionary<string, List<double>> currencies = new Dictionary<string, List<double>>();
            foreach (var key in data)
            {
                foreach (var currency in key.Value)
                {
                    Console.WriteLine($"{currency.Key}  {currency.Value}");
                    if (!currencies.ContainsKey(currency.Key))
                    {
                        currencies.Add(currency.Key, new List<double>() { currency.Value });
                    }
                    else
                    {
                        currencies[currency.Key].Add(currency.Value);
                    }
                }
            }
            Rates.currencies = new Dictionary<string, List<double>>(currencies);
        }
        
        /*
         * 
        {"rates":
            {"2020-02-24": {"EUR":0.9243852838,"RUB":65.323997042},
             "2020-02-21": {"EUR":0.9258402,"RUB":64.3903342283},
             "2020-02-20": {"EUR":0.9267840593,"RUB":63.7683039852}
             },
         "start_at":"2020-02-20","base":"USD","end_at":"2020-02-25"}

         */
    }
}
