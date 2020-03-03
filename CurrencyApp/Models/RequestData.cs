using System;
namespace CurrencyApp.Models
{
    public static class RequestData
    {
        //default values
        public static string baseUrl { get; set; } = "https://api.exchangeratesapi.io/";

        public static string historyOrLatest { get; set; } = "history?";

        public static string startAt { get; set; } = "start_at=2020-01-01&";

        public static string endAt { get; set; } = "end_at=2020-03-01&";

        public static string[] symbols { get; set; } = { "CAD", "HKD", "PHP", "DKK", "EUR", "RUB" };

        public static string baseCurrency { get; set; } = "base=USD&";

        public static string onlyOneDay { get; set; } = "";
    }
}
