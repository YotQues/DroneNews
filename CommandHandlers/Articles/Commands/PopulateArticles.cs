using Model.Entities;

namespace DroneNews.CommandHandlers.Articles.Commands;

public class PopulateArticles() : ICommand
{
    public Guid CommandId { get; } = Guid.NewGuid();
}
