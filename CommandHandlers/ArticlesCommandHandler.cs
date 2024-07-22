using CommandHandlers.Commands.Articles;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace CommandHandlers;

public class ArticlesCommandHandler(ILogger<ArticlesCommandHandler> logger, IServiceProvider serviceProvider)
{
    readonly ILogger<ArticlesCommandHandler> _logger = logger;
    readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task Handle(AddManyArticles command)
    {
        var articles = command.Articles;
        var authors = articles.Select(a => a.Author).ToList();
        var sources = articles.Select(a => a.Source).ToList();

        DroneNewsContext context = _serviceProvider.GetRequiredService<DroneNewsContext>();

        foreach (var article in articles)
        {

        }


    }
}
