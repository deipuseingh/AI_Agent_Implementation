using Microsoft.AspNetCore.Mvc;
using ECommerceApi.DTOs;
using ECommerceApi.Services;

namespace ECommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IAgentService _agentService;
    private readonly ILogger<ChatController> _logger;

    public ChatController(IAgentService agentService, ILogger<ChatController> logger)
    {
        _agentService = agentService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ChatResponseDto>> SendMessage([FromBody] ChatRequestDto request)
    {
        try
        {
            // For simplicity, we're using an empty conversation history
            // In a production system, you'd retrieve this from a database or session storage
            var conversationHistory = new List<ChatMessageDto>();

            var response = await _agentService.ProcessMessageAsync(request.Message, conversationHistory);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing chat message");
            return BadRequest(new { error = "Error processing your message" });
        }
    }

    [HttpPost("init")]
    public async Task<ActionResult<ChatResponseDto>> InitializeChat()
    {
        try
        {
            var conversationHistory = new List<ChatMessageDto>();
            var response = await _agentService.ProcessMessageAsync("INIT_CHAT", conversationHistory);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing chat");
            return BadRequest(new { error = "Error initializing chat" });
        }
    }
}
