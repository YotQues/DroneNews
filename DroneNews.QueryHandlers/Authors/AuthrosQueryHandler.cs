using DroneNews.Contract.Dto;
using DroneNews.Model;
using DroneNews.QueryHandlers.Authors.Queries;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace DroneNews.QueryHandlers.Authors;

[QueryHandler]
public class AuthrosQueryHandler(DroneNewsContext context)
{
    readonly DroneNewsContext context = context;

    public async Task<ListResponse<AuthorDto>> Handle(GetAuthorsQuery query)
    {
        var (skip, take, search) = query;

        var queryable = context.Authors.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            var searchTerm = $"%{search}%";
            queryable = queryable.Where(s => EF.Functions.Like(s.Name, search));
        }

        int totalItems = await queryable.CountAsync();
        var items = await queryable
            .Skip(skip)
            .Take(take)
            .Select(a => a.ToDto())
            .ToListAsync();

        return new ListResponse<AuthorDto>
        {
            TotalItems = totalItems,
            Items = items,
            IsLastPage = (skip + take) >= totalItems,
        };
    }
}
