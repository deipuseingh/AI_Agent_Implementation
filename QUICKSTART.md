# E-Commerce AI Agent - Quick Start Guide

Get up and running with the AI agent e-commerce system in 15 minutes.

## Prerequisites

- .NET 8.0 SDK
- Node.js 18+ with npm
- SQL Server (LocalDB)
- OpenAI API key

## Quick Start

### 1. Setup Backend (5 minutes)

```bash
# Navigate to backend
cd backend

# Restore packages
dotnet restore

# Create database
dotnet ef database update

# Run backend
dotnet run
```

✅ Backend running at `https://localhost:5001`  
✅ Swagger UI at `https://localhost:5001/swagger`

### 2. Setup Frontend (5 minutes)

```bash
# Navigate to frontend
cd frontend

# Install dependencies
npm install

# Start development server
npm start
```

✅ Frontend running at `http://localhost:3000`

### 3. Configure OpenAI (2 minutes)

Set environment variable:

```bash
# Windows PowerShell
$env:OPENAI_API_KEY = "your-key-here"

# macOS/Linux
export OPENAI_API_KEY="your-key-here"
```

## Testing the System

### Via Web UI

1. Go to `http://localhost:3000`
2. Click "Shop" tab to browse products
3. Add items to cart
4. Click "Support Chat" tab
5. Chat with the AI agent

Try these commands:

- "What's the status of order #1?"
- "Cancel order #1"
- "Can I get a refund?"
- "What products do you have?"

### Via Swagger UI

1. Go to `https://localhost:5001/swagger`
2. Expand endpoints and try them out
3. Start with `GET /api/products`
4. Then try `POST /api/chat/init` to initialize chat
5. Use `POST /api/chat` to send messages

### Test Order IDs

- Order #1: Status = Shipped (can't cancel, must return)
- Order #2: Status = Delivered (can't cancel, can refund)

## File Structure

```
backend/
├── Models/                 # Data models
├── Controllers/            # API endpoints
├── Services/               # Business logic
├── Data/                   # Database context
├── ECommerceApi.csproj    # Project file
├── Program.cs             # Startup configuration
└── appsettings.json       # Settings

frontend/
├── src/
│   ├── components/        # React components
│   ├── services/          # API client
│   ├── App.jsx            # Main app
│   └── index.jsx          # Entry point
├── package.json           # Dependencies
└── index.html             # HTML template
```

## Common Issues

### Backend won't start

```bash
# Check if LocalDB is running
sqllocaldb info

# Restart LocalDB if needed
sqllocaldb stop mssqllocaldb
sqllocaldb start mssqllocaldb
```

### Frontend can't connect to backend

```bash
# Check backend is running
# URL should be http://localhost:5000/api
# Or https://localhost:5001/api for HTTPS
```

### OpenAI API errors

```bash
# Verify API key is set
echo $env:OPENAI_API_KEY  # PowerShell

# Check API balance at openai.com
# Verify API key has correct permissions
```

## Next Steps

1. **Explore the Code**: Check `IMPLEMENTATION_GUIDE.md`
2. **Add Features**: See Phase 4 in README
3. **Deploy**: Follow production setup guides
4. **Customize**: Modify system prompt and tools

## Need Help?

See **README.md** for:

- Full architecture overview
- Complete API documentation
- Database schema details
- Advanced features guide

---

**Ready to chat with the AI?** 🚀
