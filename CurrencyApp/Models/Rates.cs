using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyApp.Models
{
    public static class Rates
    {
        public static Dictionary<DateTime, Dictionary<string, double>> Data { set; get; }

        public static List<DateTime> availableDates { set; get; }

        public static Dictionary<string, List<double>> currencies { set; get; }
    }
}
