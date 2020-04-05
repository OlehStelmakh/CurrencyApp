using System;
using System.Text;
using System.Text.Encodings.Web;
using CurrencyApp.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

public static class PagingHelpers
{
    public static HtmlString PageLinks(this IHtmlHelper html,
        NewsPageInfo pageInfo, Func<int, string> pageUrl)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 1; i <= pageInfo.TotalPages; i++)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            tag.InnerHtml.Append(i.ToString());
            if (i == pageInfo.PageNumber)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");
            result.Append(GetString(tag));
        }
        return new HtmlString(result.ToString());
    }

    public static string GetString(IHtmlContent content)
    {
        using (var writer = new System.IO.StringWriter())
        {
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}