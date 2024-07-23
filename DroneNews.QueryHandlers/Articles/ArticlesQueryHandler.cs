using DroneNews.Contract.Dto;
using DroneNews.Model;
using DroneNews.Model.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DroneNews.QueryHandlers.Articles;

[QueryHandler]
public class ArticlesQueryHandler(DroneNewsContext context, ILogger<ArticlesQueryHandler> logger)
{
    protected readonly DroneNewsContext _context = context;
    protected readonly ILogger<ArticlesQueryHandler> logger = logger;

    ~ArticlesQueryHandler()
    {
        _context.Dispose();
    }

    public async Task<ListResponse<ArticleDto>> Handle(GetArticlesQuery query)
    {
        void log(string message) => Log(nameof(GetArticlesQuery), message);
        try
        {

            log("Entered");

            var (skip, take, sourceId, authorId, search) = query;

            var baseQueryable = _context.Articles
                .Include(ar => ar.Author)
                .Include(ar => ar.Source)
                .AsQueryable();

            var predicate = LinqKit.PredicateBuilder.New<Article>(true);

            log("Finished Init");
            if (sourceId != null)
            {
                predicate = predicate.And(ar => ar.SourceId == sourceId);
            }
            if (authorId != null)
            {
                predicate = predicate.And(ar => ar.AuthorId == authorId);
            }
            if (!string.IsNullOrEmpty(search))
            {
                var searchTerm = search.Trim().ToLower();
                predicate = predicate
                    .And(ar => EF.Functions.Like(ar.Title.ToLower(), $"%{searchTerm}%"))
                    .Or(ar => EF.Functions.Contains(ar.Description.ToLower(), searchTerm));
            }

            baseQueryable = baseQueryable
                .Where(predicate);

            log("Compiled predicate");

            int totalItems = await baseQueryable.CountAsync();
            log("Retreived Total Items Count");
            ArticleDto[] items = await baseQueryable
                        .Skip(skip)
                        .Take(take)
                        .Select(ar => ar.ToDto())
                        .ToArrayAsync();

            log("Retreived Paginated Items - Success");
            return new ListResponse<ArticleDto>
            {
                TotalItems = totalItems,
                Items = items,
                IsLastPage = (skip + take) >= totalItems
            };

        }
        catch (Exception ex)
        {
            log($"Failed - {ex.Message}");
            throw;
        }
    }
    private void Log(string queryName, string message)
    {
        logger.LogInformation($"[ {nameof(ArticlesQueryHandler)} / ${queryName} ] {message}");
    }
}


