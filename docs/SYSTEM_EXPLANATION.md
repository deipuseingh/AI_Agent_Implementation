# Comprehensive System Explanation & Knowledge Base

This document provides a deep dive into how the Agentic AI E-Commerce system is built, the components involved, the workflow from start to finish, and the technical knowledge required to maintain and extend it.

---

## 1. How the System is Made (Architecture Overview)

This system is built using a **decoupled, full-stack architecture**. It separates the user interface (Frontend) from the business logic and database (Backend), while introducing a middle intelligence layer (AI Agent) to handle natural language customer support.

The architecture follows this primary flow:
`User (React UI)` <--> `REST API (ASP.NET Core)` <--> `AI Agent (Semantic Kernel)` <--> `Database (SQLite)`

### The Core Components:

1. **Frontend (The User Interface)**
   - **Built with:** React 18, Tailwind CSS, Vite.
   - **Purpose:** Provides the sandbox environment where users can view products, manage a shopping cart, and interact with the AI Chat Agent. It communicates with the backend exclusively via HTTP REST API calls.

2. **Backend (The Core Engine & API)**
   - **Built with:** C#, .NET 8.0, ASP.NET Core Web API.
   - **Purpose:** Handles all data processing, enforces business rules, manages API routing (e.g., `/api/products`, `/api/chat`), and connects to the database.

3. **Database (Data Persistence)**
   - **Built with:** SQLite and Entity Framework (EF) Core.
   - **Purpose:** Stores persistent data such as Users, Products, Orders, and Order Items using relational tables. EF Core acts as the ORM (Object-Relational Mapper) to convert C# code into SQL queries automatically.

4. **AI Agent Layer (The Intelligence)**
   - **Built with:** Microsoft Semantic Kernel, Google Gemini API (or OpenAI).
   - **Purpose:** Parses natural language input from the user, decides which backend functions need to be executed to fulfill the request, and responds in a conversational tone.

---

## 2. Implementation & Workflow of the AI Agent

The AI Agent in this project is not just a standard chatbot; it is an **Agentic System**. This means it has agency—the ability to take actions (like canceling orders or checking statuses) by executing your backend code via a concept called **Function Calling** (or Tool Calling).

### Step-by-Step Workflow:

1. **Chat Initialization:**
   - When the user opens the frontend chat, the React app sends an `"INIT_CHAT"` string to the backend.
   - The backend's `AgentService.cs` intercepts this and immediately returns a **Generative UI Menu** (buttons like "Check Order Status") via the `DisplayActionMenu` tool.

2. **User Sends a Message:**
   - The user types: _"I want to check the status of my 4k monitor order."_
   - The frontend sends this message, along with previous conversation history, to the backend's `/api/chat` endpoint.

3. **Prompt Construction:**
   - Inside `AgentService.cs`, the system builds a "ChatHistory" object.
   - It injects a **System Prompt** (instructions telling the AI it is an e-commerce support agent and outlining its rules).

4. **Tool Registration (Semantic Kernel):**
   - Semantic Kernel bundles the user's prompt alongside a list of available tools. These tools are defined in `AgentToolService.cs` (e.g., `FindOrderByProductNameAsync`, `ProcessRefundAsync`).
   - Notice how every C# function has a `[Description]` attribute. The LLM reads these descriptions to understand _what_ the tool does.

5. **The Function Calling Loop:**
   - The backend sends the prompt + tools to Google Gemini.
   - Gemini realizes: _"The user is asking about a 4k monitor. I should use the `FindOrderByProductName` tool!"_
   - Instead of generating a text reply, Gemini replies to Semantic Kernel asking it to execute that specific tool.
   - Semantic Kernel automatically runs the C# code inside `FindOrderByProductNameAsync("4k monitor")`, queries the SQLite database, and gets the result (e.g., "Order #2, Status: Shipped").
   - Semantic Kernel sends this database result _back_ to Gemini.

6. **Final AI Response:**
   - Gemini receives the database data, formulates a natural, polite response (_"I found your 4k monitor! It is currently Shipped..."_), and sends the final string back to the React UI.

---

## 3. Understanding CORS (Cross-Origin Resource Sharing)

In this system, you will notice a specific CORS configuration in `Program.cs`.

**What is CORS?**
CORS is a security mechanism built into modern web browsers. By default, a browser prevents a website running on one domain (e.g., `http://localhost:3000` - your React app) from making HTTP requests to a different domain (e.g., `https://localhost:5001` - your C# backend). This is called the Same-Origin Policy.

**Why do we need it?**
Because our frontend and backend run on different ports during development, the browser considers them different "origins". Without CORS, the browser would block the frontend's API calls (like fetching products or sending chat messages).

**How it is implemented:**
In `Program.cs`, we explicitly tell the backend to trust the frontend using:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Trust this exact URL
              .AllowAnyHeader()                     // Allow any HTTP headers (like Content-Type)
              .AllowAnyMethod();                    // Allow GET, POST, PUT, DELETE
    });
});
```

_Note: If you ever host this project on a live server (like Vercel and Azure), you must update the `WithOrigins` URL to your actual live frontend domain._

---

## 4. Required Knowledge Base

To fully understand, maintain, and expand this project, you should be familiar with the following concepts:

### A. C# & .NET 8 (Backend)

- **Dependency Injection (DI):** Understanding how `builder.Services.AddScoped<IAgentService, AgentService>()` works to provide classes with the services they need.
- **Entity Framework Core:** How models (classes) map to database tables, and how LINQ (`.Where()`, `.FirstOrDefaultAsync()`) is used to query the SQLite database.
- **Asynchronous Programming:** Using `async` and `await` with `Task<T>` so the server can handle multiple requests without freezing while waiting for the database or AI API.
- **DTOs (Data Transfer Objects):** Why we use specific classes (like `ChatRequestDto`) to control exactly what data comes in and out of the API.

### B. Microsoft Semantic Kernel (AI Orchestration)

- **Plugins & Kernel Functions:** How standard C# methods are converted into LLM tools using the `[KernelFunction]` attribute.
- **Auto-Invocation:** Understanding how `GeminiToolCallBehavior.AutoInvokeKernelFunctions` tells the framework to handle the back-and-forth tool execution loop automatically.
- **ChatHistory & Prompts:** How system instructions and user history are concatenated to give the LLM memory and boundaries.

### C. React (Frontend)

- **Hooks:** Specifically `useState` (managing current UI data like the cart or chat messages) and `useEffect` (triggering code when a component loads, like fetching products).
- **Props & Component Composition:** How smaller UI pieces (like `ChatInterface.jsx`) are passed data from their parent (`App.jsx`).

### D. Web APIs

- **HTTP Methods:** GET (retrieve data), POST (create data / send chat), PUT (update), DELETE (remove).
- **JSON Serialization:** How C# objects are converted to JSON text before being sent over the network, and how circular references (Order -> User -> Order) are handled using `ReferenceHandler.IgnoreCycles`.

---

## Summary

By combining an ASP.NET Core backend to manage secure data, a React frontend for user interaction, and Semantic Kernel to bridge the gap between AI generation and C# execution, this system represents a modern, state-of-the-art **Agentic Application**.
