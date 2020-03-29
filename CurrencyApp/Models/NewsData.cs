using System;
using System.Collections.Generic;

namespace CurrencyApp.Models
{
    public class NewsData : IComparable
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

        public int CompareTo(object obj)
        {
            NewsData other = obj as NewsData;
            if (other != null)
            {
                int i = publishedAt.Value.CompareTo(other.publishedAt.Value);
                if (i == -1)
                    return 1;
                else if (i == 1)
                    return -1;
                else
                    return i;
            }
            else
            {
                throw new Exception("Impossible to compare two objects");
            }
        }
    }

    public static class News
    {
        public static SortedSet<NewsData> allNewsData { get; set; } = new SortedSet<NewsData>();

        public static int totalAmountOfResults { get; set; }
    }
}
