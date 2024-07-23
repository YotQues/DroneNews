
namespace DroneNews.QueryHandlers.Articles;

public record GetArticlesQuery(int Skip = 0, int Take = 20, int? SourceId = null, int? AuthorId = null, string? Search = null) : IQuery
{
    public Guid QueryId { get; } = Guid.NewGuid();

    public int Skip = Skip;
    public int Take = Take;
    public int? SourceId = SourceId;
    public int? AuthorId = AuthorId;
    public string? Search = Search;
}
