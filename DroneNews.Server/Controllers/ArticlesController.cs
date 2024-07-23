using DroneNews.Contract.Dto;
using DroneNews.QueryHandlers.Articles;
using Microsoft.AspNetCore.Mvc;

namespace DroneNews.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController(ArticlesQueryHandler queryHandler, ILogger<ArticlesController> logger) : ControllerBase
{
    private readonly ArticlesQueryHandler queryHandler = queryHandler;
    private readonly ILogger<ArticlesController> _logger = logger;

    [HttpGet(Name = "FetchArticles")]
    [ProducesResponseType(typeof(ListResponse<AuthorDto>), 200)]
    public async Task<IActionResult> GetArticles([FromQuery] GetArticlesQuery query)
    {
        void log(string message) => Log(HttpContext.Request.Path, HttpContext.Request.Method, message);
        try
        {
            log("Entered");
            var (search, source, author, page) = query;

            log("Sending Query");
            int pageSize = 20;
            var res = await queryHandler.Handle(new((page - 1) * pageSize, pageSize, source, author, search));

            log("Success");
            return Ok(res);

        }
        catch (Exception ex)
        {
            log($"Failed: {ex.Message}");
            return StatusCode(500, new { ex.Message });
        }
    }
    private void Log(string route, string method, string message)
    {

        _logger.LogInformation($"[{method}] / {route} - {message}");
    }

}

public record GetArticlesQuery(string? search = null, int? source = null, int? author = null, int page = 1);
