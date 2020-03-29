using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using CurrencyApp.Models;

namespace CurrencyApp.Controllers
{
    public class NewsController : Controller
    {
        public static void getDataFromAPI()
        {
            // init with your API key
            var newsApiClient = new NewsApiClient(NewsRequestData.apiKEY);
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = NewsRequestData.keyWord,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = NewsRequestData.fromDate,
                PageSize=100
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                // total results found
                News.totalAmountOfResults = articlesResponse.TotalResults;
                int id = 0;
                // here's the first 20
                foreach (var article in articlesResponse.Articles)
                {
                    if (article.UrlToImage==null) {
                        article.UrlToImage = "https://smallbusinessfirst.com.au/media/397512/finance-courses.jpg?width=600&height=400&mode=crop";
                    }
                    News.allNewsData.Add(
                        new NewsData(article.Title, article.Author, article.Description,
                        article.Url, article.UrlToImage, article.PublishedAt, id++));
                }

                News.allNewsData.Sort((x, y) => x.publishedAt.Value.CompareTo(y.publishedAt.Value));
                News.allNewsData.Reverse();
            }
        }

        public IActionResult TakeNews()
        {
            getDataFromAPI();
            return View();
        }
    }
}
