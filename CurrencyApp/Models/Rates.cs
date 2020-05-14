using System;
using System.Collections.Generic;

namespace CurrencyApp.Models
{
    public class Rates
    {
        private static Rates _instance;

        private Rates()
        {

        }

        public static Rates GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Rates();
            }
            return _instance;
        }

        public static SortedDictionary<DateTime, Dictionary<string, double>> Data { set; get; }

        public static List<DateTime> availableDates { set; get; }

        public static SortedDictionary<string, List<double>> currencies { set; get; }
    }
}

