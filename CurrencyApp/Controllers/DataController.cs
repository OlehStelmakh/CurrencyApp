using Microsoft.AspNetCore.Mvc;
using CurrencyApp.Models;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Text;
using CurrencyApp.Shared;

namespace CurrencyApp.Controllers
{
    public class DataController : Controller
    {
        [HttpGet]
        public IActionResult TakeData()
        {
            string requestMessage = "https://api.exchangeratesapi.io/latest";
            string responseMessage = AdditionalMethods.createResponse(requestMessage);
            ViewBag.availableCurrencies = parseCurrenciesNames(responseMessage);
            return View();
        }

        public static string[] parseCurrenciesNames(string responseMessage) 
        {
            var jsonObject = JObject.Parse(responseMessage);
            var lastUpdates = jsonObject["rates"];

            List<string> currencies = new List<string>();

            foreach (JProperty currency in lastUpdates)
            {
                if (!currencies.Contains(currency.Name))
                {
                    currencies.Add(currency.Name);
                }
            }

            string baseCurrency = jsonObject["base"].ToString();
            currencies.Add(baseCurrency);

            return currencies.ToArray();
        }
    }
}
