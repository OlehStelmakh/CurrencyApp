using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CurrencyApp.Models
{
    public class RequestData
    {
        //default values
        public static string baseUrl { get; set; } = "https://api.exchangeratesapi.io/";

        public static string historyOrLatest { get; set; } = "history?";

        [Required]
        public string startAt { get; set; } = "2020-01-01";

        [Required]
        public string endAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        public static List<string> symbols { get; set; } = new List<string> { "CAD", "HKD", "PHP", "DKK", "RUB", "USD" };

        [Required]
        public string baseCurrency { get; set; } = "USD";

        public static string onlyOneDay { get; set; } = "";
    }
}
