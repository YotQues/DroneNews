using DroneNews.Contract.Dto;
using DroneNews.Model;
using DroneNews.QueryHandlers.Sources.Queries;
using System.Data.Entity;

namespace DroneNews.QueryHandlers.Sources;

[QueryHandler]
public class SourcesQueryHandler(DroneNewsContext context)
{
    readonly DroneNewsContext context = context;

    public async Task<ListResponse<SourceDto>> Handle(GetSourcesQuery query)
    {
        var (skip, take) = query;

        var queryable = context.Sources.AsQueryable();

        int totalItems = await queryable.CountAsync();
        var items = await queryable
            .Skip(skip)
            .Take(take)
            .Select(s => s.ToDto())
            .ToListAsync();

        return new ListResponse<SourceDto>
        {
            TotalItems = totalItems,
            Items = items,
            IsLastPage = (skip + take) >= totalItems,
        };

    }
}
