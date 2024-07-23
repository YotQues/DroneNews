using DroneNews.Contract.Dto;
using DroneNews.QueryHandlers.Sources;
using Microsoft.AspNetCore.Mvc;

namespace DroneNews.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController(SourcesQueryHandler queryHandler, ILogger<SourcesController> logger) : ControllerBase
    {
    private readonly SourcesQueryHandler queryHandler = queryHandler;
        private readonly ILogger<SourcesController> _logger = logger;

    [HttpGet(Name = "FetchSources")]
    [ProducesResponseType(typeof(ListResponse<SourceDto>), 200)]
    public async Task<IActionResult> GetSources([FromQuery] GetSourcesQuery query)
    {
        try
        {
            Log("Entered");
            var (page, pageSize, search) = query;

            Log("Sending Query");
            var res = await queryHandler.Handle(new((page - 1) * pageSize, pageSize, search));

            Log("Success");
            return Ok(res);

        }
        catch (Exception ex)
        {
            Log($"Failed: {ex.Message}");
            return StatusCode(500, new { ex.Message });
        }
    }
    private void Log(string message)
    {

        _logger.LogInformation($"[{HttpContext.Request.Path}] / {HttpContext.Request.Method} - {message}");
    }

    }
}
public record GetSourcesQuery(int page = 1, int pageSize = 10, string? search = null);
