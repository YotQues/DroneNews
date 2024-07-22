using Model.Entities;

namespace CommandHandlers.Commands.Articles;

public class AddManyArticles(IEnumerable<Article> articles) : ICommand
{
    public Guid CommandId { get; } = Guid.NewGuid();
    public IEnumerable<Article> Articles { get; } = articles;
}
