using System;
using System.Collections.Generic;

namespace CurrencyApp.Models
{
    public class NewsPageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
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
