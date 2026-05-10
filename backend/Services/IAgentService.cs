using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApi.DTOs;

namespace ECommerceApi.Services;

public interface IAgentService
{
    Task<ChatResponseDto> ProcessMessageAsync(string message, List<ChatMessageDto> conversationHistory);
}