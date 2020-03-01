using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyApp.Models
{
    public class Rates
    {
        public static SortedDictionary<DateTime, Dictionary<string, double>> Data { set; get; }

        public static List<DateTime> availableDates { set; get; }

        public static SortedDictionary<string, List<double>> currencies { set; get; }
    }
}
