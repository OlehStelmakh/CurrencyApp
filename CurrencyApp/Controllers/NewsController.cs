using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using CurrencyApp.Models;
using System.Collections.Generic;

namespace CurrencyApp.Controllers
{
    public class NewsController : Controller
    {
        public static void getDataFromAPI()
        {
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
                    News.allNewsData.Add(
                        new NewsData(article.Title, article.Author, article.Description,
                        article.Url, article.UrlToImage, article.PublishedAt, id++));
                }
            }
        }

        public IActionResult TakeNews()
        {
            getDataFromAPI();
            return View();
        }
    }
}
