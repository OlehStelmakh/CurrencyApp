using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CurrencyApp.Models;
using System.Net;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web;
using System.Text;
using CurrencyApp.Shared;

namespace CurrencyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Rates rates = new Rates();
            return View(rates);
        }

        [HttpPost]
        public IActionResult Index(List<string> inlineCheckbox, DateTime firstDate, DateTime lastDate, string baseCurrency)
        {
            prepareRequestData(inlineCheckbox, firstDate, lastDate, baseCurrency, out bool swapped);
            ViewBag.swapped = swapped;
            string request = CreateRequest();
            string response = AdditionalMethods.createResponse(request);
            analyzeResponseData(response);
            createDataLists(Rates.Data);
            Rates rates = new Rates();
            return View(rates);
        }

        public static void prepareRequestData(List<string> inlineCheckbox,
            DateTime firstDate, DateTime lastDate, string baseCurrency, out bool swapped)
        {
            swapped = false;
            if (firstDate.CompareTo(lastDate) > 0)
            {
                DateTime temp = firstDate;
                firstDate = lastDate;
                lastDate = temp;
                swapped = true;
            }
            RequestData.symbols = inlineCheckbox;
            if (RequestData.symbols.Contains(baseCurrency))
            {
                RequestData.symbols.Remove(baseCurrency);
            }
            RequestData.startAt = "start_at=" + firstDate.ToString("yyyy-MM-dd") + "&";
            RequestData.endAt = "end_at=" + lastDate.ToString("yyyy-MM-dd") + "&";
            RequestData.baseCurrency = "base=" + baseCurrency + "&";
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

        public static string CreateRequest()
        {
            StringBuilder stringBuilder = new StringBuilder(150);
            stringBuilder.Append(RequestData.baseUrl);
            if (!string.IsNullOrEmpty(RequestData.onlyOneDay))
            {
                stringBuilder.Append(RequestData.onlyOneDay);
                return stringBuilder.ToString();
            }
            stringBuilder.Append(RequestData.historyOrLatest);
            stringBuilder.Append(RequestData.startAt);
            stringBuilder.Append(RequestData.endAt);

            for (int i = 0; i<RequestData.symbols.Count; i++)
            {
                if (i==0)
                {
                    stringBuilder.Append("symbols=");
                }
                if (i==RequestData.symbols.Count - 1)
                {
                    stringBuilder.Append(RequestData.symbols[i] + "&");
                    break;
                }
                stringBuilder.Append(RequestData.symbols[i] + ",");
            }
            stringBuilder.Append(RequestData.baseCurrency);
            return stringBuilder.ToString();
        }

        public static void analyzeResponseData(string jsonData)
        {
            Rates.Data = new SortedDictionary<DateTime, Dictionary<string, double>>();
            
            var jsonObject = JObject.Parse(jsonData);

            if (!string.IsNullOrEmpty(RequestData.startAt))
            {
                CurrencyData.startAt = getDateFromString(jsonObject["start_at"].ToString());
            }
            if (!string.IsNullOrEmpty(RequestData.endAt))
            {
                CurrencyData.endAt = getDateFromString(jsonObject["end_at"].ToString());
            }
            if (!string.IsNullOrEmpty(RequestData.baseCurrency))
            {
                CurrencyData.baseCurrency = jsonObject["base"].ToString();
            }
            
            List<string> currencies = new List<string>();
            if (RequestData.historyOrLatest.Contains("latest"))
            {
                var lastUpdates = jsonObject["rates"];
                Dictionary<string, double> keyValues = new Dictionary<string, double>();

                foreach (JProperty currency in lastUpdates)
                {
                    if (!currencies.Contains(currency.Name))
                    {
                        currencies.Add(currency.Name);
                    }
                    keyValues.Add(currency.Name, double.Parse(currency.Value.ToString()));
                }
                Rates.Data.Add(DateTime.Now, keyValues);
                CurrencyData.symbols = currencies;
                return;
            }
            var days = jsonObject["rates"];
            foreach (JProperty day in days) {

                DateTime dateTime = getDateFromString(day.Name);
                Dictionary<string, double> keyValues = new Dictionary<string, double>();
                foreach (JProperty currency in day.Value)
                {
                    if (!currencies.Contains(currency.Name))
                    {
                        currencies.Add(currency.Name);
                    }
                    keyValues.Add(currency.Name, double.Parse(currency.Value.ToString()));
                }
                Rates.Data.Add(dateTime, keyValues);
            }
            CurrencyData.symbols = currencies;
        }

        public static DateTime getDateFromString(string date)
        {
            int year = int.Parse(date.Substring(0, 4));
            int month = int.Parse(date.Substring(5, 2));
            int day = int.Parse(date.Substring(8, 2));
            return new DateTime(year, month, day);
        }

        public static void createDataLists(SortedDictionary<DateTime, Dictionary<string, double>> data)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (var key in data)
            {
                dates.Add(key.Key);
            }
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
            Rates.currencies = new SortedDictionary<string, List<double>>(currencies);
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
