using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using CurrencyApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyApp.Controllers
{
    public class NewsController : Controller
    {
        public static void getDataFromAPI()
        {
            SortedSet<NewsData> allNewsData = new SortedSet<NewsData>();
            var newsApiClient = new NewsApiClient(NewsRequestData.apiKEY);
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = NewsRequestData.keyWord,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = NewsRequestData.fromDate,
                PageSize = NewsRequestData.PageSize
            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                News.totalAmountOfResults = articlesResponse.TotalResults;
                int id = 0;

                foreach (var article in articlesResponse.Articles)
                {
                    if (article.UrlToImage == null)
                    {
                        article.UrlToImage = "https://smallbusinessfirst.com.au/media/397512/finance-courses.jpg?width=600&height=400&mode=crop";
                    }
                    allNewsData.Add(
                        new NewsData(article.Title, article.Author, article.Description,
                        article.Url, article.UrlToImage, article.PublishedAt, id++));
                }
            }

            News.allNewsData = new List<NewsData>(allNewsData);
        }

        [HttpGet]
        public IActionResult TakeNews(int page = 1)
        {
            if (!News.NewsReceived)
            {
                getDataFromAPI();
                News.NewsReceived = true;
            }

            int pageSize = 20;
            IEnumerable<NewsData> newsPerPages = News.allNewsData.Skip((page - 1) * pageSize).Take(pageSize);
            NewsPageInfo pageInfo = new NewsPageInfo { PageNumber = page, PageSize = pageSize, TotalItems = News.allNewsData.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, News = newsPerPages };
            return View(ivm);
        }

        [HttpPost]
        public IActionResult TakeNews()
        {
            News.NewsReceived = false;
            return TakeNews(1);
        }
    }
}
