using System;
using System.Collections.Generic;

namespace CurrencyApp.Models
{
    public class NewsData
    {
        public string title { get; }
        public string author { get; }
        public string description { get; }
        public string url { get; }
        public string urlToImage { get; }
        public DateTime? publishedAt { get; }
        public int id { get;  }

        public NewsData(string Title, string Author, string Description,
            string Url, string UrlToImage, DateTime? PublishedAt, int Id)
        {
            title = Title;
            author = Author;
            description = Description;
            url = Url;
            urlToImage = UrlToImage;
            publishedAt = PublishedAt;
            id = Id;
        }
    }

    public static class News
    {
        public static List<NewsData> allNewsData { get; set; } = new List<NewsData>();

        public static int totalAmountOfResults { get; set; }
    }
}
