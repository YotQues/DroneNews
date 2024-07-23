namespace DroneNews.CommandHandlers.Articles;

public class PopulateArticles() : ICommand
{
    public Guid CommandId { get; } = Guid.NewGuid();
}
