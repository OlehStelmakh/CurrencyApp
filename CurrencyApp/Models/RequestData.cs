using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CurrencyApp.Models
{
    public static class RequestData
    {
        //default values
        public static string baseUrl { get; set; } = "https://api.exchangeratesapi.io/";

        public static string historyOrLatest { get; set; } = "history?";

        [Required]
        public static string startAt { get; set; } = "start_at=2020-01-01&";

        [Required]
        public static string endAt { get; set; } = "end_at=" + DateTime.Now.ToString("yyyy-MM-dd") + "&";

        [Required]
        public static List<string> symbols { get; set; } = new List<string> { "CAD", "HKD", "PHP", "DKK", "RUB", "USD" };

        [Required]
        public static string baseCurrency { get; set; } = "base=USD&";

        public static string onlyOneDay { get; set; } = "";
    }
}
