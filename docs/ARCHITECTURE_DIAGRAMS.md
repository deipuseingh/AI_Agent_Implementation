# System Architecture Diagrams

## Complete System Architecture

```
┌────────────────────────────────────────────────────────────────────────────┐
│                           USER INTERACTION LAYER                           │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                            │
│  ┌──────────────────────────┐          ┌──────────────────────────┐        │
│  │   Product Catalog Page   │          │    Chat Interface Page   │        │
│  │                          │          │                          │        │
│  │  • Product Grid          │          │  • Message Display       │        │
│  │  • Price Display         │          │  • Action Menu Buttons   │        │
│  │  • Stock Status          │          │  • Input Field           │        │
│  │  • Add to Cart           │          │  • Send Button           │        │
│  └──────────────────────────┘          └──────────────────────────┘        │
│                                                                            │
│  ┌──────────────────────────────────────────────────────────────┐          │
│  │              Shopping Cart (Persistent Sidebar)              │          │
│  │  • Item List with Quantities                                 │          │
│  │  • Remove Items                                              │          │
│  │  • Total Calculation                                         │          │
│  │  • Checkout Button                                           │          │
│  └──────────────────────────────────────────────────────────────┘          │
│                                                                            │
│  ┌──────────────────────────────────────────────────────────────┐          │
│  │         Navigation Bar (Products / Support Chat)             │          │
│  └──────────────────────────────────────────────────────────────┘          │
│                                                                            │
└────────────────────────────────────────────────────────────────────────────┘
                                    ↕ HTTP/REST
                              (axios / fetch API)
┌────────────────────────────────────────────────────────────────────────────┐
│                         BACKEND API LAYER (ASP.NET Core)                   │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                            │
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐          │
│  │ ProductsCtrl     │  │   OrdersCtrl     │  │  ChatController  │          │
│  │                  │  │                  │  │                  │          │
│  │ GET /products    │  │ GET /orders/{id} │  │ POST /chat       │          │
│  │ POST /products   │  │ POST /orders     │  │ POST /chat/init  │          │
│  └──────────────────┘  │ POST /cancel     │  └──────────────────┘          │
│                        │ POST /refund     │                                │
│                        └──────────────────┘                                │
│                                                                            │
│  ┌────────────────────────────────────────────────────────────────┐        │
│  │                    SERVICE LAYER                               │        │
│  │                                                                │        │
│  │  ┌──────────────────────────┐  ┌──────────────────────────┐    │        │
│  │  │  IAgentToolService       │  │   IAgentService          │    │        │
│  │  │                          │  │                          │    │        │
│  │  │ • CheckOrderStatus()     │  │ • ProcessMessage()       │    │        │
│  │  │ • CancelOrder()          │  │ • HandleToolCall()       │    │        │
│  │  │ • ProcessRefund()        │  │ • GenerateResponse()     │    │        │
│  │  │ • ListProducts()         │  │                          │    │        │
│  │  │ • DisplayActionMenu()    │  │                          │    │        │
│  │  └──────────────────────────┘  └──────────────────────────┘    │        │
│  │                                                                │        │
│  └────────────────────────────────────────────────────────────────┘        │
│                                                                            │
│  ┌────────────────────────────────────────────────────────────────┐        │
│  │         SEMANTIC KERNEL + OPENAI INTEGRATION (Phase 3)         │        │
│  │                                                                │        │
│  │  • LLM Connector (OpenAI)                                      │        │
│  │  • Function Definition Manager                                 │        │
│  │  • Tool Call Router                                            │        │
│  │  • Conversation Loop Manager                                   │        │
│  │  • Prompt Engineering                                          │        │
│  │                                                                │        │
│  └────────────────────────────────────────────────────────────────┘        │
│                                                                            │
│  ┌────────────────────────────────────────────────────────────────┐        │
│  │             ENTITY FRAMEWORK CORE (Data Access)                │        │
│  │                                                                │        │
│  │  DbContext: ECommerceDbContext                                 │        │
│  │  • DbSet<Product>  DbSet<Order>  DbSet<OrderItem>              │        │
│  │  • DbSet<User>                                                 │        │
│  │  • Migrations Management                                       │        │
│  │  • Query Execution                                             │        │
│  │                                                                │        │
│  └────────────────────────────────────────────────────────────────┘        │
│                                                                             │
└─────────────────────────────────────────────────────────────────────────────┘
                                    ↕ SQL
                         (Parameterized Queries)
┌─────────────────────────────────────────────────────────────────────────────┐
│                    DATABASE LAYER (SQLite)                                  │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│  ┌─────────────┐  ┌──────────────┐  ┌──────────────┐  ┌─────────────────┐   │
│  │    Users    │  │   Products   │  │    Orders    │  │   OrderItems    │   │
│  │             │  │              │  │              │  │                 │   │
│  │ • Id        │  │ • Id         │  │ • Id         │  │ • Id            │   │ 
│  │ • Email     │  │ • Name       │  │ • UserId(FK) │  │ • OrderId(FK)   │   │ 
│  │ • Name      │  │ • Price      │  │ • Status     │  │ • ProductId(FK) │   │
│  │ • Phone     │  │ • Stock      │  │ • Dates      │  │ • Quantity      │   │
│  │ • CreatedAt │  │ • CreatedAt  │  │ • Amount     │  │ • UnitPrice     │   │
│  │             │  │              │  │              │  │                 │   │
│  │ (2 records) │  │ (10 records) │  │ (2 records)  │  │ (3 records)     │   │
│  └─────────────┘  └──────────────┘  └──────────────┘  └─────────────────┘   │
│                                                                             │
│  Foreign Keys: Orders.UserId → Users.Id                                     │
│                OrderItems.OrderId → Orders.Id                               │
│                OrderItems.ProductId → Products.Id                           │ 
│                                                                             │
└─────────────────────────────────────────────────────────────────────────────┘
                                    ↕ API
                         (Function Calling)
┌─────────────────────────────────────────────────────────────────────────────┐
│                     EXTERNAL AI SERVICE (OpenAI API)                        │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                              │
│  • GPT-4o Language Model                                                    │
│  • Function Calling Support                                                 │
│  • Natural Language Understanding                                            │
│  • Intent Recognition & Extraction                                          │
│  • Response Generation                                                       │
│                                                                              │
└─────────────────────────────────────────────────────────────────────────────┘
```

---

## Function Calling Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                         USER SENDS MESSAGE                          │
│                    "Cancel order #54321"                            │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│              CHAT CONTROLLER RECEIVES REQUEST                       │
│         POST /api/chat { "message": "Cancel order #54321" }        │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│            AGENT SERVICE PROCESSES MESSAGE                          │
│                                                                     │
│  1. Parse message                                                   │
│  2. Recognize intent (CANCEL)                                       │
│  3. Extract parameters (orderId: 54321)                             │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│         SEMANTIC KERNEL SENDS REQUEST TO OPENAI                     │
│                                                                     │
│  {                                                                  │
│    "model": "gpt-4",                                                │
│    "messages": [...],                                               │
│    "tools": [                                                       │
│      { "name": "CancelOrder", "parameters": {...} },                │
│      { "name": "CheckOrderStatus", "parameters": {...} }            │
│    ]                                                                │
│  }                                                                  │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│               OPENAI LLM DECIDES & RESPONDS                         │
│                                                                     │
│  {                                                                  │
│    "tool_calls": [                                                  │
│      {                                                              │
│        "function": "CancelOrder",                                   │
│        "arguments": { "orderId": 54321 }                            │
│      }                                                              │
│    ]                                                                │
│  }                                                                  │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│       AGENT SERVICE ROUTES TO TOOL HANDLER                          │
│                                                                     │
│  if (toolCall.function === "CancelOrder") {                         │
│    return await toolService.CancelOrderAsync(54321);                │
│  }                                                                  │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│         AGENT TOOL SERVICE EXECUTES THE FUNCTION                    │
│                                                                     │
│  1. Check if order exists (SELECT FROM Orders WHERE Id = 54321)     │
│  2. Verify order status (if Shipped, can't cancel)                  │
│  3. Update status to "Canceled"                                     │
│  4. Set CanceledDate = NOW()                                        │
│  5. Save changes to database                                        │
│                                                                     │
│  Result: {                                                          │
│    "success": true,                                                 │
│    "message": "Order canceled successfully"                         │
│  }                                                                  │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│       AGENT SERVICE SENDS RESULT BACK TO OPENAI                     │
│                                                                     │
│  {                                                                  │
│    "role": "tool",                                                  │
│    "tool_call_id": "call_123",                                      │
│    "content": "Order canceled successfully"                         │
│  }                                                                  │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│            OPENAI GENERATES FINAL RESPONSE                          │
│                                                                     │
│  "I have successfully canceled order #54321 for you.               │
│   A refund will be processed within 5-7 business days."            │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│         AGENT SERVICE RETURNS RESPONSE TO CONTROLLER                │
│                                                                     │
│  {                                                                  │
│    "content": "I have successfully canceled order...",              │
│    "toolCall": null                                                 │
│  }                                                                  │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│         CHAT CONTROLLER RETURNS RESPONSE TO CLIENT                  │
│                                                                     │
│  HTTP 200 OK                                                        │
│  {                                                                  │
│    "content": "I have successfully canceled order #54321...",       │
│    "toolCall": null                                                 │
│  }                                                                  │
└────────────────────────────┬────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────────────┐
│         FRONTEND DISPLAYS RESPONSE TO USER                          │
│                                                                     │
│  "I have successfully canceled order #54321 for you.               │
│   A refund will be processed within 5-7 business days."            │
│                                                                     │
│  ✅ Order status updated in database                               │
│  ✅ User informed of cancellation                                  │
│  ✅ Conversation continues naturally                               │
└─────────────────────────────────────────────────────────────────────┘
```

---

## Generative UI (Action Menu) Flow

```
┌──────────────────────────────────────────────────────┐
│     USER OPENS CHAT INTERFACE ON FRONTEND            │
│                                                      │
│  ChatInterface.jsx mounts                           │
│  → calls useEffect() → calls initializeChat()        │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   FRONTEND SENDS INIT_CHAT MESSAGE                   │
│                                                      │
│  POST /api/chat/init                                │
│  (silent message to backend)                        │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   BACKEND AI RECEIVES INIT_CHAT                      │
│                                                      │
│  AgentService.ProcessMessageAsync("INIT_CHAT")      │
│  → Recognizes it's chat start                       │
│  → Reads system prompt                              │
│  → Decides to show menu                             │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   AI CALLS DisplayActionMenu TOOL                    │
│                                                      │
│  {                                                   │
│    "functionName": "DisplayActionMenu",             │
│    "arguments": {                                   │
│      "greetingText": "How can I help?",             │
│      "options": [                                   │
│        "Check Order Status",                        │
│        "Cancel Order",                              │
│        "Process Refund",                            │
│        "View Products",                             │
│        "Talk to Support"                            │
│      ]                                              │
│    }                                                │
│  }                                                  │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   BACKEND RETURNS TOOL CALL TO FRONTEND              │
│                                                      │
│  HTTP 200 OK                                        │
│  {                                                  │
│    "content": "",                                   │
│    "toolCall": { /* as above */ }                   │
│  }                                                  │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   FRONTEND INTERCEPTS TOOL CALL                      │
│                                                      │
│  ChatInterface.jsx receives response                │
│  → Checks if toolCall.functionName ===              │
│    "DisplayActionMenu"                              │
│  → YES! Special handling needed                     │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   FRONTEND RENDERS ACTION MENU (NOT BUTTONS)         │
│                                                      │
│  ┌──────────────────────────────────┐               │
│  │  How can I help?                 │               │
│  │                                  │               │
│  │  [Check Order Status]            │               │
│  │  [Cancel Order]                  │               │
│  │  [Process Refund]                │               │
│  │  [View Products]                 │               │
│  │  [Talk to Support]               │               │
│  └──────────────────────────────────┘               │
│                                                      │
│  React Code:                                        │
│  message.options.map(option => (                    │
│    <button onClick={() =>                           │
│      sendMessage(option)}>                          │
│      {option}                                       │
│    </button>                                        │
│  ))                                                 │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   USER CLICKS ACTION BUTTON                          │
│                                                      │
│  User clicks: "Cancel Order"                        │
│  → onClick triggers sendMessage("Cancel Order")     │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   BUTTON TEXT SENT AS REGULAR MESSAGE                │
│                                                      │
│  POST /api/chat                                     │
│  {                                                  │
│    "message": "Cancel Order"                        │
│  }                                                  │
│                                                      │
│  Frontend also renders:                             │
│  "Cancel Order" message in chat window              │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   AGENT RECOGNIZES USER INTENT                       │
│                                                      │
│  Backend receives "Cancel Order"                    │
│  → Recognizes intent (CANCEL)                       │
│  → Continues conversation naturally                 │
│  → Replies: "I can help with that. Could you       │
│    please provide your Order ID?"                   │
└────────────────────┬─────────────────────────────────┘
                     ↓
┌──────────────────────────────────────────────────────┐
│   CONVERSATION CONTINUES NORMALLY                    │
│                                                      │
│  Frontend displays agent response                   │
│  User types: "Order #54321"                         │
│  Menu disappears → regular chat continues          │
│  → User keeps chatting naturally                    │
│  → No more buttons needed                           │
│  → Context is maintained                           │
└──────────────────────────────────────────────────────┘
```

---

## Component Hierarchy (Frontend)

```
App (Main Shell)
├── Header
│   ├── Logo
│   ├── Navigation Tabs
│   │   ├── Shop Tab
│   │   └── Support Chat Tab
│   ├── Mobile Menu Toggle
│   └── Cart Button (with badge)
│
├── Main Content Area
│   ├── ProductCatalog (when "Shop" tab active)
│   │   ├── ProductGrid
│   │   │   └── ProductCard[] (repeating)
│   │   │       ├── ProductImage
│   │   │       ├── ProductName
│   │   │       ├── ProductPrice
│   │   │       ├── StockStatus
│   │   │       └── AddToCartButton
│   │   └── Error/Loading States
│   │
│   ├── MyOrders (when "My Orders" tab active)
│   │   └── OrderCard[] (repeating)
│   │       ├── OrderID
│   │       ├── OrderStatus
│   │       ├── TotalAmount
│   │       └── OrderItemsList
│   │
│   └── ChatInterface (when "Support Chat" tab active)
│       ├── MessagesContainer
│       │   └── Message[] (repeating)
│       │       ├── UserMessage
│       │       ├── AssistantMessage
│       │       └── ActionMenu (conditional)
│       │           └── ActionButton[] (repeating)
│       │
│       └── InputContainer
│           ├── InputField
│           └── SendButton
│
└── CartSidebar (when cart is open)
    ├── CartHeader
    ├── CartItems[] (repeating)
    │   ├── ItemImage
    │   ├── ItemName
    │   ├── ItemQuantity
    │   ├── ItemPrice
    │   └── RemoveButton
    │
    ├── CartTotal
    └── CheckoutButton
```

---

## State Management (Frontend)

```
App Component State:
├── currentView: "products" | "chat"
├── cartItems: Product[]
│   └── { id, name, price, quantity }
└── showCart: boolean

ProductCatalog State:
├── products: Product[]
├── loading: boolean
└── error: string | null

ChatInterface State:
├── messages: ChatMessage[]
│   └── { id, role, content, toolCall?, isMenu?, options? }
├── inputValue: string
└── loading: boolean
```

---

## Data Flow

```
User Input (UI)
        ↓
React Component (State Update)
        ↓
API Client (HTTP Request)
        ↓
Backend Controller
        ↓
Service Layer
        ↓
Tool Service / Agent Service
        ↓
Entity Framework Core
        ↓
Database Query/Update
        ↓
Response Object
        ↓
Serialize to JSON
        ↓
HTTP Response
        ↓
React Component (State Update)
        ↓
Component Re-render
        ↓
Updated UI
```

---

These diagrams show the complete architecture and data flow of the system. Each layer is designed to be modular and testable.
