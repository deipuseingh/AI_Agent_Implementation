# E-Commerce AI Agent System

A complete implementation of an agentic AI e-commerce system with function calling, natural language processing, and a generative UI. This system demonstrates how to build a decoupled architecture where an AI agent acts as a middleman between users and backend systems.

## 📋 Project Overview

### Architecture Components

1. **Frontend (React/Next.js)**: Sandbox UI with product catalog and persistent chat window
2. **Backend (ASP.NET Core)**: Core API managing products, orders, users, and AI operations
3. **Database (SQLite)**: Relational database for order state management
4. **AI Agent Layer (Semantic Kernel)**: Intelligent layer using function calling with OpenAI GPT-4o

## 🚀 Features

### Phase 1: Foundation (Completed)

- ✅ ASP.NET Core Web API setup with Entity Framework Core
- ✅ Database models: Product, Order, OrderItem, User
- ✅ CRUD endpoints for products and orders
- ✅ Seed data with 10 dummy products and sample orders
- ✅ React frontend with product catalog
- ✅ Shopping cart functionality

### Phase 2: AI Agent Integration (In Progress)

- ✅ Agent system prompt and rules
- ✅ Tool definitions for backend operations
- ✅ Chat endpoint with message processing
- ✅ Function calling loop implementation
- ⏳ Semantic Kernel integration

### Phase 3: Generative UI (In Progress)

- ✅ DisplayActionMenu tool definition
- ✅ Chat interface with button rendering
- ✅ Dynamic menu generation based on conversation context
- ⏳ Advanced context awareness

### Phase 4: Advanced Features (Planned)

- ⏳ RAG for policy enforcement
- ⏳ Sentiment analysis
- ⏳ Automatic discount application
- ⏳ Persistent conversation history

## 📦 Tech Stack

### Backend

- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQLite
- **AI Framework**: Microsoft Semantic Kernel
- **LLM Provider**: OpenAI API (GPT-4o)

### Frontend

- **Library**: React 18.2
- **Styling**: Tailwind CSS
- **Icons**: Lucide React
- **HTTP Client**: Axios
- **Build Tool**: Vite (recommended)

## 🛠️ Setup Instructions

### Prerequisites

- .NET 8.0 SDK
- Node.js 18+
- SQLite (Supported out-of-the-box by EF Core)
- OpenAI API key

### Backend Setup

1. Navigate to backend folder:

   ```bash
   cd backend
   ```

2. Restore NuGet packages:

   ```bash
   dotnet restore
   ```

3. Create and seed database:

   ```bash
   dotnet ef database update
   ```

4. Set environment variables:

   ```bash
   $env:OPENAI_API_KEY = "your-openai-api-key"
   ```

5. Run the backend:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:5001` and Swagger UI at `https://localhost:5001/swagger`

### Frontend Setup

1. Navigate to frontend folder:

   ```bash
   cd frontend
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Create `.env.local` file:

   ```
   REACT_APP_API_URL=http://localhost:5000/api
   ```

4. Start development server:
   ```bash
   npm start
   ```

The frontend will be available at `http://localhost:3000`

## 🎯 System Workflow

### Function Calling Process

1. **User Input**: User sends message via chat interface
2. **AI Processing**: Backend passes message to OpenAI with function definitions
3. **Function Selection**: LLM decides which backend function to call
4. **Execution**: Backend executes the function and returns result
5. **Response Loop**: AI processes result and generates user-friendly response
6. **UI Rendering**: Response is sent to frontend and rendered

### Example: Cancel Order Flow

```
User: "Cancel order #54321"
    ↓
Backend (Agent): Receives message, recognizes cancel intent
    ↓
LLM Function Call: Calls CancelOrder(orderId: 54321)
    ↓
Backend Execution: Updates order status to "Canceled"
    ↓
Response: "I have successfully canceled order #54321..."
    ↓
Frontend: Displays confirmation message to user
```

## 📊 API Endpoints

### Products

- `GET /api/products` - List all products
- `GET /api/products/{id}` - Get product details
- `POST /api/products` - Create product

### Orders

- `GET /api/orders/{id}/status` - Get order status
- `POST /api/orders` - Create order
- `POST /api/orders/{id}/cancel` - Cancel order
- `POST /api/orders/{id}/refund` - Process refund
- `GET /api/orders/user/{userId}` - Get user orders

### Chat

- `POST /api/chat` - Send message to AI agent
- `POST /api/chat/init` - Initialize chat with greeting

## 🤖 AI Agent Tools

### Available Tools

1. **DisplayActionMenu**: Shows interactive action buttons
   - Parameters: greetingText, options[]
2. **CheckOrderStatus**: Retrieve order details
   - Parameters: orderId
3. **CancelOrder**: Cancel an order (if Processing)
   - Parameters: orderId
4. **ProcessRefund**: Process refund (if Delivered or Processing)
   - Parameters: orderId, reason
5. **ListProducts**: Display available products
   - No parameters

## 💬 System Prompt

The AI agent operates under a strict system prompt that defines:

- Persona: Helpful customer support agent
- Rules: Professional tone, use tools appropriately, validate Order IDs
- Boundaries: Won't make up data, guides users back to options
- Capabilities: Can check status, cancel, refund, and manage orders

## 🔒 Security Considerations

### Implemented

- CORS enabled for frontend-backend communication
- Database migrations for controlled schema updates
- Input validation on backend
- Type safety with C# strongly-typed models

### Recommended for Production

- API authentication (OAuth 2.0 or JWT)
- Rate limiting
- HTTPS enforcement
- Database encryption
- OpenAI API key management (AWS Secrets Manager, Azure Key Vault)
- Request logging and monitoring
- SQL injection prevention (using EF Core parameterized queries)

## 📝 Database Schema

### Users Table

- Id (PK)
- Email (unique)
- Name
- Phone (nullable)
- CreatedAt

### Products Table

- Id (PK)
- Name
- Description
- Price
- ImageUrl
- StockQuantity
- CreatedAt

### Orders Table

- Id (PK)
- UserId (FK)
- OrderDate
- Status (Processing, Shipped, Delivered, Canceled, Returned)
- TotalAmount
- ShippedDate (nullable)
- DeliveredDate (nullable)
- CanceledDate (nullable)
- LastModified

### OrderItems Table

- Id (PK)
- OrderId (FK)
- ProductId (FK)
- Quantity
- UnitPrice

## 🎨 UI Components

### Frontend Components

- **App.jsx**: Main application shell, top-level state management (Cart, Views), and view routing.
- **ProductCatalog.jsx**: Grid of products fetched from the backend with "Add to Cart" triggers.
- **ChatInterface.jsx**: Renders AI message history, inputs, and parses generative UI action menus.
- **MyOrders.jsx**: Renders the user's previously placed orders and triggers support chat views.
- **apiClient.js**: API communication service centralizing all frontend REST requests.

### Features

- Responsive design (mobile, tablet, desktop)
- Shopping cart with persistent display
- Real-time chat interface
- Dynamic action button generation
- Loading states and error handling

## 🚧 Next Steps (Phase 3 & 4)

1. **Enhanced Semantic Kernel Integration**
   - Full function calling with proper JSON schemas
   - Conversation context management
   - Streaming responses

2. **RAG Implementation**
   - Store return policies in vector database
   - Implement policy checking before refunds
   - Create knowledge base for FAQs

3. **Sentiment Analysis**
   - Integrate Azure Text Analytics or similar
   - Detect frustrated customers
   - Auto-apply discount codes

4. **Persistence**
   - Store conversation history in database
   - User session management
   - Order interaction history

## 🐛 Troubleshooting

### Database Connection Issues

- Ensure LocalDB is running: `sqllocaldb info`
- Check connection string in `appsettings.json`
- Verify user has database permissions

### Frontend-Backend Communication

- Ensure backend is running on expected port
- Check CORS settings in Program.cs
- Verify API_URL in frontend .env

### OpenAI API Errors

- Verify API key is set correctly
- Check API rate limits
- Review API usage and billing

## 📖 Documentation

### System Architecture Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                      Frontend (React)                        │
│  ┌──────────────────┐        ┌───────────────────────────┐  │
│  │  Product Catalog │        │   Chat Interface          │  │
│  │  Shopping Cart   │        │  Action Menu Buttons      │  │
│  └──────────────────┘        └───────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                          ↓↑ HTTP/REST
┌─────────────────────────────────────────────────────────────┐
│              Backend (ASP.NET Core API)                      │
│  ┌──────────────┐  ┌──────────────┐  ┌───────────────────┐  │
│  │ Controllers  │  │  Services    │  │  Database Context │  │
│  │ - Products   │  │ - Agent      │  │  (Entity Frame)   │  │
│  │ - Orders     │  │ - Tools      │  │                   │  │
│  │ - Chat       │  │              │  │                   │  │
│  └──────────────┘  └──────────────┘  └───────────────────┘  │
│         ↓                ↓                                    │
│  ┌────────────────────────────────────────────────────────┐  │
│  │        Semantic Kernel + OpenAI Integration            │  │
│  │  - Function Calling Framework                          │  │
│  │  - Tool Definition & Execution                         │  │
│  │  - Conversation Loop Management                        │  │
│  └────────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                          ↓↑ SQL
┌─────────────────────────────────────────────────────────────┐
│           Database (SQL Server / PostgreSQL)                │
│  - Users | Products | Orders | OrderItems                  │
└─────────────────────────────────────────────────────────────┘

                          ↕ API Calls
┌─────────────────────────────────────────────────────────────┐
│            OpenAI API (GPT-4o)                               │
│  - Function Definition Processing                           │
│  - LLM Inference & Tool Selection                           │
│  - Semantic Understanding                                   │
└─────────────────────────────────────────────────────────────┘
```

## 📄 License

This project is provided as-is for educational and demonstration purposes.

## 🤝 Support

For issues or questions:

1. Check the Troubleshooting section
2. Review API error logs in backend
3. Check browser console for frontend errors
4. Verify all environment variables are set correctly

---

**Happy Building! 🚀**
