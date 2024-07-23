
namespace DroneNews.QueryHandlers.Authors.Queries;

public record GetAuthorsQuery(int Skip = 0, int Take = 20) : IQuery
{
    public Guid QueryId { get; } = Guid.NewGuid();
}
