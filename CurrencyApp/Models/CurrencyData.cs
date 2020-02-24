using System;
using System.Collections.Generic;
namespace CurrencyApp.Models
{
    public static class CurrencyData
    {
        public static DateTime startAt { set; get; }
        public static DateTime endAt { set; get; }
        public static string baseCurrency {set; get;}
        public static Dictionary<string, double> symbols { set; get; }
    }
}
