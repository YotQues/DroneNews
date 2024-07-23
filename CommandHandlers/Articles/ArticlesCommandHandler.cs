using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using Newtonsoft.Json;
using DroneNews.Services.NewsApi;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DroneNews.CommandHandlers.Articles.Commands;
using CommandHandlers;

namespace DroneNews.CommandHandlers.Articles;

[CommandHandler]
public class ArticlesCommandHandler(ILogger<ArticlesCommandHandler> _logger, IServiceProvider serviceProvider)
{
    readonly ILogger<ArticlesCommandHandler> logger = _logger;
    readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task Handle(PopulateArticles command)
    {
        void log(string message) => Log(nameof(PopulateArticles), message);

        log("Entered");

        try
        {
            using DroneNewsContext context = _serviceProvider.GetRequiredService<DroneNewsContext>();

            log("Dependencies Injected");

            var lastDate = context.Articles.Any() ? (await context.Articles.OrderBy(ar => ar.PublishedAt).FirstAsync()).PublishedAt : DateTime.UtcNow;
            var minDate = DateTime.UtcNow.AddMonths(-1).AddDays(1);

            if (lastDate <= minDate)
            {
                log("Up to date");
                return;
            }

            log("Requesting Articles");

            var articlesResponse = await FetchArticles(minDate, lastDate);

            if (!articlesResponse.Articles.Any())
            {
                log("No articlies found");
                return;
            }

            await ProcessArticles(context, articlesResponse, log);

        }
        catch (Exception ex)
        {
            log($"Failed: {JsonConvert.SerializeObject(ex)}");
        }


    }

    public async Task Handle(QueryForArticleUpdates command)
    {
        void log(string message) => Log(nameof(QueryForArticleUpdates), message);

        log("Entered");

        try
        {
            using DroneNewsContext context = _serviceProvider.GetRequiredService<DroneNewsContext>();

            log("Dependencies Injected");

            var lastDate = context.Articles.Any() ? (await context.Articles
                .OrderByDescending(ar => ar.PublishedAt)
                .FirstAsync()).PublishedAt : DateTime.UtcNow.AddMonths(-1).AddDays(1);

            log("Requesting Articles");

            var articlesResponse = await FetchArticles(lastDate.AddSeconds(1), DateTime.UtcNow);

            if (!articlesResponse.Articles.Any())
            {
                log("No articlies found");
                return;
            }

            await ProcessArticles(context, articlesResponse, log);
        }
        catch (Exception ex)
        {
            log($"Failed: {JsonConvert.SerializeObject(ex)}");
        }


    }

    private async Task ProcessArticles(DroneNewsContext context, ArticlesResult articlesResponse, Action<string> log)
    {
        var sources = await context.Sources.ToListAsync();
        log("Fetched existing sources");
        var authors = await context.Authors.ToListAsync();
        log("Fetched existing authors");

        List<Model.Entities.Article> newArticles = [];
        Dictionary<string, Model.Entities.Author> newAuthors = [];
        Dictionary<string, Model.Entities.Source> newSources = [];

        foreach (var article in articlesResponse.Articles)
        {
            string sourceName = article.Source.Name.ToLower().Trim();

            var source = sources.FirstOrDefault(s => s.Url.ToLower().Trim() == sourceName);
            if (source == null && !newSources.TryGetValue(sourceName, out source))
            {
                source = new Model.Entities.Source { Url = article.Source.Name };
                newSources[sourceName] = source;
            }

            Model.Entities.Author? author = null;

            if (article.Author != null)
            {

                string authorName = article.Author.ToLower().Trim();

                author = authors.FirstOrDefault(au => au.Name.ToLower().Trim() == authorName);

                if (author == null && !newAuthors.TryGetValue(authorName, out author))
                {
                    author = new Model.Entities.Author { Name = article.Author };
                    newAuthors[authorName] = author;
                }
            }

            var articleRow = new Model.Entities.Article
            {
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                ImageUrl = article.UrlToImage,
                OriginalUrl = article.Url,
                PublishedAt = article.PublishedAt ?? DateTime.UtcNow,
                Author = author,
                Source = source
            };

            newArticles.Add(articleRow);
        }

        if (newAuthors.Count > 0)
            context.Authors.AddRange(newAuthors.Values);
        if (newSources.Count > 0)
            context.Sources.AddRange(newSources.Values);

        if (newSources.Count > 0 || newAuthors.Count > 0)
            await context.SaveChangesAsync();

        context.Articles.AddRange(newArticles);
        await context.SaveChangesAsync();
        log("Added articles successfully");
    }

    private async Task<ArticlesResult> FetchArticles(DateTime minDate, DateTime? lastDate)
    {
        NewsApiClient client = _serviceProvider.GetRequiredService<NewsApi>().client;
        ArticlesResult articlesResponse = await client.GetEverythingAsync(new EverythingRequest
        {
            Q = "drone",
            SortBy = SortBys.PublishedAt,
            Language = Languages.EN,
            From = minDate,
            To = lastDate,
            PageSize = 100,
        });

        if (articlesResponse.Status != Statuses.Ok)
        {
            var message = $"Request Failed: {articlesResponse.Error}";
            throw new Exception(message);
        }

        return articlesResponse;
    }

    private void Log(string command, string message)
    {

        logger.LogInformation($"[{nameof(ArticlesCommandHandler)} / {command} ] - {message}");
    }
}
