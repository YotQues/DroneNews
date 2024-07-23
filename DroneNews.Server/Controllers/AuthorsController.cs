using DroneNews.Contract.Dto;
using DroneNews.QueryHandlers.Authors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DroneNews.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController(AuthrosQueryHandler queryHandler, ILogger<AuthorsController> logger) : ControllerBase
{
private readonly AuthrosQueryHandler queryHandler = queryHandler;
    private readonly ILogger<AuthorsController> _logger = logger;

[HttpGet(Name = "FetchAuthors")]
[ProducesResponseType(typeof(ListResponse<AuthorDto>), 200)]
public async Task<IActionResult> GetAuthors([FromQuery] GetAuthorsQuery query)
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
public record GetAuthorsQuery(int page = 1, int pageSize = 10, string? search = null);
