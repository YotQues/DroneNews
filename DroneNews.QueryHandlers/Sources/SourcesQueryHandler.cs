using DroneNews.Contract.Dto;
using DroneNews.Model;
using DroneNews.QueryHandlers.Sources.Queries;
using Microsoft.EntityFrameworkCore;

namespace DroneNews.QueryHandlers.Sources;

[QueryHandler]
public class SourcesQueryHandler(DroneNewsContext context)
{
    readonly DroneNewsContext context = context;
    ~SourcesQueryHandler()
    {
        context.Dispose();
    }
    public async Task<ListResponse<SourceDto>> Handle(GetSourcesQuery query)
    {
        var (skip, take, search) = query;

        var queryable = context.Sources.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            var searchTerm = search.ToLower().Trim();
            queryable = queryable
                .Where(s => s.Url.ToLower().Trim().Contains(searchTerm) ||
                EF.Functions.Like(s.Url.ToLower().Trim(), $"%{searchTerm}%"));
        }

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
