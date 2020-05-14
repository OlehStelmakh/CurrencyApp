using System;
using System.Collections.Generic;

namespace CurrencyApp.Models
{
    public class NewsPageInfo
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int TotalItems { get; set; } 
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    public class IndexViewModel
    {
        public IEnumerable<NewsData> News { get; set; }
        public NewsPageInfo PageInfo { get; set; }
    }
}
