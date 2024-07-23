using CommandHandlers.Commands;

namespace DroneNews.CommandHandlers.Articles.Commands;

public class QueryForArticleUpdates : ICommand
{
    public Guid CommandId { get; } = Guid.NewGuid();
}
