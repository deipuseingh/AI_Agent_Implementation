using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using System.Text.Json;
using ECommerceApi.DTOs;

namespace ECommerceApi.Services;

public class AgentService : IAgentService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chatCompletion;
    private readonly IAgentToolService _toolService;
    private readonly ILogger<AgentService> _logger;

    public AgentService(
        Kernel kernel,
        IChatCompletionService chatCompletion,
        IAgentToolService toolService,
        ILogger<AgentService> logger)
    {
        _kernel = kernel;
        _chatCompletion = chatCompletion;
        _toolService = toolService;
        _logger = logger;

        // Give the AI access to your backend tools
        _kernel.Plugins.AddFromObject(_toolService, "ECommerceTools");
    }

    public async Task<ChatResponseDto> ProcessMessageAsync(string message, List<ChatMessageDto> conversationHistory)
    {
        try
        {
            // 1. Handle Generative UI action menu
            if (message.Equals("INIT_CHAT", StringComparison.OrdinalIgnoreCase))
            {
                var menu = _toolService.GetActionMenu();
                return new ChatResponseDto
                {
                    Content = "",
                    ToolCall = new ToolCallDto
                    {
                        FunctionName = "DisplayActionMenu",
                        Arguments = new Dictionary<string, object>
                        {
                            { "greetingText", menu.GreetingText },
                            { "options", menu.Options }
                        }
                    }
                };
            }

            // 2. Build the System Prompt
            var chatHistory = new ChatHistory(
                "You are an intelligent, polite customer support agent for an e-commerce store. " +
                "You have tools to manage orders, process refunds, and list products. " +
                "Always ask the user for their Order ID or the product name before attempting to look up, cancel, or refund an order. " +
                "Do not make up order data; rely entirely on the results of your tools.");

            // Add the previous conversation history so the LLM remembers context
            foreach (var msg in conversationHistory ?? new List<ChatMessageDto>())
            {
                if (msg.Role == "user") chatHistory.AddUserMessage(msg.Content);
                else if (msg.Role == "assistant") chatHistory.AddAssistantMessage(msg.Content);
            }

            chatHistory.AddUserMessage(message);

            // 3. Configure the LLM to automatically call your tools (functions)
            var executionSettings = new GeminiPromptExecutionSettings
            {
                ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
            };

            // 4. Send the message to Gemini and wait for it to execute tools and generate a response
            var response = await _chatCompletion.GetChatMessageContentAsync(
                chatHistory,
                executionSettings,
                _kernel);

            return new ChatResponseDto
            {
                Content = response.Content ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing chat message via LLM");
            
            if (ex.Message.Contains("429"))
            {
                return new ChatResponseDto { Content = "I'm receiving too many requests right now and hit my rate limit. Please wait a minute and try again!" };
            }

            return new ChatResponseDto { Content = $"I'm having trouble connecting to my AI brain right now. Please try again. (Debug Error: {ex.Message})" };
        }
    }
}
