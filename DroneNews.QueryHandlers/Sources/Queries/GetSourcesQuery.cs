namespace DroneNews.QueryHandlers.Sources.Queries;

public record GetSourcesQuery(int Skip = 0, int Take = 20, string? Search = null) : IQuery
{
    public Guid QueryId { get; } = Guid.NewGuid();
}
