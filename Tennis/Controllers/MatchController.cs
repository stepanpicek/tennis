using Microsoft.AspNetCore.Mvc;
using Tennis.Dto.Requests;
using Tennis.Services;

namespace Tennis.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;
    private readonly ILogger<MatchController> _logger;

    public MatchController(ILogger<MatchController> logger, IMatchService matchService)
    {
        _logger = logger;
        _matchService = matchService;
    }

    [HttpPost(Name = "start")]
    public async Task<IActionResult> StartMatch([FromBody] StartMatchRequest request)
    {
        try
        {
            await _matchService.StartMatchAsync(request);
            _logger.LogInformation("Match {name} started.", request.Name);
            return Ok();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet(Name = "progress/{name}")]
    public async Task<IActionResult> GetMatchProgress([FromQuery] string name)
    {
        try
        {
            var response = await _matchService.GetMatchProgressAsync(name);
            return Ok(response);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}