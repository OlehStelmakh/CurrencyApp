using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyApp.Models
{
    public class CurrencyData
    {
        public static DateTime startAt { set; get; }
        public static DateTime endAt { set; get; }
        public static string baseCurrency {set; get;}
        public static List<string> symbols { set; get; }
    }
}
