
namespace DroneNews.CommandHandlers.Articles;
public class QueryForArticleUpdates : ICommand
{
    public Guid CommandId { get; } = Guid.NewGuid();
}
