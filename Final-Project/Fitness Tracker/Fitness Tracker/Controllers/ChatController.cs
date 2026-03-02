using Microsoft.AspNetCore.Mvc;
using Fitness_Tracker.Agents;
using Microsoft.AspNetCore.Antiforgery;

namespace Fitness_Tracker.Controllers;

[Route("api/chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IOpenAiService _openAi;

    public ChatController(IOpenAiService openAi)
    {
        _openAi = openAi;
    }

    public record ChatRequest(string Question);

    [HttpPost("ask")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Ask([FromBody] ChatRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Question))
            return BadRequest(new { error = "Question is required." });

        var answer = await _openAi.GetChatCompletionAsync(request.Question);
        return Ok(new { answer });
    }
}
