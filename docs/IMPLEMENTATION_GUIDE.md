# AI Agent Implementation Guide

This document provides detailed implementation guidance for the agentic AI e-commerce system.

## Table of Contents

1. [System Architecture Overview](#system-architecture-overview)
2. [Function Calling Mechanism](#function-calling-mechanism)
3. [Implementation Phases](#implementation-phases)
4. [Generative UI (Server-Driven UI)](#generative-ui)
5. [Advanced Features](#advanced-features)

## System Architecture Overview

### Decoupled Architecture Pattern

The system follows a **decoupled architecture** where the AI agent acts as a middleman:

```
User (Chat Interface) → AI Agent (Decision Making) → Backend APIs → Database
```

### Key Components

#### 1. Frontend (The Sandbox)

A simple web interface with two main views:

- **Product Catalog**: Display products with "Buy Now" buttons
- **Chat Window**: Persistent chat to interact with AI agent

#### 2. Backend (Core API)

The central nervous system providing:

- REST endpoints for products, orders, and users
- Agent endpoints for specific operations (cancel, refund, status check)
- Chat endpoint for AI agent communication
- Database management

#### 3. Database

Relational database storing:

- Products (catalog data)
- Orders (transaction state)
- OrderItems (line items)
- Users (customer information)

#### 4. AI Agent Layer

Intelligence layer featuring:

- Natural language understanding
- Intent recognition
- Function calling
- Response generation

## Function Calling Mechanism

### How It Works

Function calling (or Tool Calling) is the bridge that allows LLMs to execute actions:

```
1. User: "Cancel order #54321"
   ↓
2. Agent: Recognizes intent = cancel, extracts ID = 54321
   ↓
3. LLM Output: { "function": "CancelOrder", "orderId": 54321 }
   ↓
4. Backend: Executes SQL update, changes order status to "Canceled"
   ↓
5. Agent: Reads success result
   ↓
6. Response: "I've successfully canceled order #54321 for you"
   ↓
7. User: Sees confirmation message
```

### Tool Definition Schema

Each tool must be defined with:

```json
{
  "name": "CancelOrder",
  "description": "Cancels an order if it hasn't been shipped",
  "parameters": {
    "type": "object",
    "properties": {
      "orderId": {
        "type": "integer",
        "description": "The ID of the order to cancel"
      }
    },
    "required": ["orderId"]
  }
}
```

## Implementation Phases

### Phase 1: Build Foundation (✅ Completed)

#### Step 1: Setup ASP.NET Web API

```bash
dotnet new webapi -n ECommerceApi
cd ECommerceApi
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.SemanticKernel
```

#### Step 2: Define Models

Create models with proper relationships:

- Product: Name, Price, StockQuantity
- Order: Status (Processing/Shipped/Delivered/Canceled/Returned)
- OrderItem: ProductId, Quantity, UnitPrice
- User: Email, Name, Phone

#### Step 3: Seed Data

Database includes:

- 10 dummy products (Laptop, Mouse, Monitor, etc.)
- 2 sample users
- 2 sample orders (one Shipped, one Delivered)

#### Step 4: Create Endpoints

```
GET    /api/products              - List all products
GET    /api/products/{id}         - Get product details
POST   /api/orders                - Create order
GET    /api/orders/{id}/status    - Check order status
POST   /api/orders/{id}/cancel    - Cancel order
POST   /api/orders/{id}/refund    - Process refund
POST   /api/chat                  - Chat with agent
POST   /api/chat/init             - Initialize chat
```

### Phase 2: Create Sandbox UI (✅ In Progress)

#### Product Page

```javascript
// Grid display of products
- Show product name, price, stock
- "Add to Cart" button
- Sends POST to /api/orders
- Returns order ID to user
```

#### Chat Interface

```javascript
// Standard chat window with:
- Message display area
- Input field
- Send button
- Handles tool responses
- Renders action menus
```

### Phase 3: Build AI Agent (🚧 In Progress)

#### System Prompt

```
You are the intelligent customer support agent for our e-commerce store.
Your primary goal is to help users manage their orders efficiently.

RULES:
1. Always maintain polite, professional tone
2. You have access to: DisplayActionMenu, CheckOrderStatus, CancelOrder,
   ProcessRefund, ListProducts
3. Always ask for Order ID before taking action
4. Don't make up data - rely only on backend tools
5. If unrelated question, guide back to available options
```

#### Tool Implementation in Backend

**AgentToolService.cs**:

```csharp
public class AgentToolService : IAgentToolService
{
    // CheckOrderStatus - Query database for order info
    public async Task<string> CheckOrderStatusAsync(int orderId)

    // CancelOrder - Update status if not shipped
    public async Task<string> CancelOrderAsync(int orderId)

    // ProcessRefund - Update status and log refund
    public async Task<string> ProcessRefundAsync(int orderId, string reason)

    // ListProducts - Query and format product list
    public async Task<string> ListProductsAsync()

    // DisplayActionMenu - Return menu options
    public ActionMenuResult GetActionMenu()
}
```

#### Execution Loop

```csharp
public async Task<ChatResponseDto> ProcessMessageAsync(string message)
{
    // 1. Send message + tool definitions to LLM
    // 2. LLM returns tool call or text response
    // 3. If tool call:
    //    a. Execute the tool
    //    b. Return result to LLM
    //    c. LLM generates final response
    // 4. Return response to user
}
```

### Phase 4: Advanced Scenarios (⏳ Planned)

#### Policy Enforcement (RAG)

```csharp
// Load return policy from document
var policy = await _policyService.GetReturnPolicy();

// Use RAG to check if return is allowed
var isAllowed = await _ragService.CheckPolicy(
    orderId: 54321,
    policy: policy,
    reason: "Item is defective"
);

// Only allow if policy permits
if (isAllowed)
    await _toolService.ProcessRefundAsync(orderId, reason);
```

#### Sentiment Analysis

```csharp
// Analyze customer sentiment
var sentiment = await _sentimentService.AnalyzeAsync(userMessage);

if (sentiment.IsVeryFrustrated)
{
    // Auto-apply discount code
    await _dbContext.ApplyDiscountCodeAsync(userId, code: "SORRY10");
    response += "\nI've applied a 10% discount code for you: SORRY10";
}
```

## Generative UI (Server-Driven UI)

### The Concept

Instead of hardcoding buttons on frontend, the AI decides when to show them:

```
1. Frontend loads chat → sends INIT_CHAT to backend
2. Backend AI reads prompt → decides to show menu
3. AI calls DisplayActionMenu tool
4. Frontend intercepts response → renders buttons
5. User clicks button → sends as regular message
6. Cycle repeats with context awareness
```

### Implementation

#### Backend: Tool Definition

```csharp
KernelFunctionFactory.CreateFromMethod(
    async () => await DisplayActionMenuAsync(),
    "DisplayActionMenu",
    "Displays clickable action buttons"
)
```

#### Frontend: Response Handler

```javascript
if (message.toolCall?.functionName === "DisplayActionMenu") {
  return (
    <div className="agent-menu">
      <p>{message.toolCall.arguments.greetingText}</p>
      <div className="button-grid">
        {message.toolCall.arguments.options.map((option) => (
          <button onClick={() => sendMessage(option)}>{option}</button>
        ))}
      </div>
    </div>
  );
}
```

### Dynamic Menu Example

```javascript
// Chat initialization
User Opens Chat
    ↓
AI receives INIT_CHAT
    ↓
AI calls DisplayActionMenu with:
    greetingText: "Hello! How can I help?"
    options: [
        "Check Order Status",
        "Cancel Order",
        "Process Refund",
        "View Products",
        "Talk to Support"
    ]
    ↓
Frontend renders buttons
    ↓
User clicks "Cancel Order"
    ↓
AI asks for Order ID
    ↓
Menu disappears, conversation continues naturally
```

## Advanced Features

### 1. Conversation Context

Store conversation history to understand context:

```csharp
var conversationHistory = new List<ChatMessage>
{
    new ChatMessage(ChatRole.User, "Cancel order #54321"),
    new ChatMessage(ChatRole.Assistant, "I can help with that..."),
    new ChatMessage(ChatRole.User, "My Order ID is 54321")
};

// LLM understands context from previous messages
await _kernel.InvokeAsync("Chat", new KernelArguments
{
    ["history"] = conversationHistory,
    ["userMessage"] = "Go ahead and cancel it"
});
```

### 2. Error Handling & Validation

```csharp
// Validate before tool execution
if (!IsValidOrderId(orderId))
    return "Please provide a valid Order ID";

// Handle business logic
if (order.Status == "Shipped")
    return "Cannot cancel shipped order, offer return instead";

// Graceful error responses
try
{
    // Execute tool
}
catch (Exception ex)
{
    _logger.LogError(ex, "Tool execution failed");
    return "I encountered an error. Please try again.";
}
```

### 3. Multi-turn Conversation

```
User: "What's my order status?"
Agent: "I'd be happy to help! What's your Order ID?"
User: "54321"
Agent: [Calls CheckOrderStatus(54321)]
Agent: "Your order #54321 is currently Shipped..."
User: "Can I cancel it?"
Agent: "Unfortunately, your order has already shipped. Would you like to initiate a return instead?"
```

### 4. Personality & Tone

```
System Prompt Controls:
- Formality level: professional, friendly, casual
- Response length: concise, detailed, explanatory
- Emoji usage: yes/no, limited
- Language: English, Spanish, etc.
```

## Security Best Practices

### 1. Input Validation

```csharp
// Validate all inputs before processing
if (!int.TryParse(orderId, out var id) || id <= 0)
    return "Invalid Order ID";
```

### 2. Authorization

```csharp
// Verify user owns the order
var order = await _dbContext.Orders
    .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

if (order == null)
    return "Order not found";
```

### 3. Rate Limiting

```csharp
// Prevent abuse
[RateLimit(requests: 10, perSeconds: 60)]
public async Task<ChatResponseDto> SendMessage([FromBody] ChatRequestDto request)
```

### 4. Sensitive Data

```csharp
// Never expose sensitive data
var dto = new OrderStatusDto
{
    OrderId = order.Id,
    Status = order.Status,
    TotalAmount = order.TotalAmount,
    // Don't include: PaymentDetails, CustomerPhone, etc.
};
```

## Testing the System

### Manual Testing Scenarios

1. **Check Order Status**
   - Send: "What's the status of order #1?"
   - Expected: Order details displayed

2. **Cancel Order**
   - Send: "Cancel order #1"
   - Expected: "Cannot cancel shipped order..."
3. **Menu Initialization**
   - Send: "help" or "what can you do?"
   - Expected: Action menu buttons appear

4. **Error Handling**
   - Send: "Cancel order #99999"
   - Expected: "Order not found"

### API Testing with Swagger

```bash
# Swagger UI available at
https://localhost:5001/swagger

# Test endpoints:
1. GET /api/products - verify 10 products returned
2. POST /api/orders - create test order
3. POST /api/chat/init - verify menu returned
4. POST /api/chat - test message handling
```

---

This implementation guide provides the foundation for building a complete agentic AI system. Each phase builds on the previous, progressively adding complexity and features.
