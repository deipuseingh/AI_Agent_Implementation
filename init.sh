#!/bin/bash

# Script to initialize the entire project
# Supports Windows, macOS, and Linux

set -e

echo "🚀 E-Commerce AI Agent - Project Initialization"
echo "================================================"

# Check prerequisites
echo "📋 Checking prerequisites..."

if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET SDK not found. Please install .NET 8.0"
    exit 1
fi

if ! command -v node &> /dev/null; then
    echo "❌ Node.js not found. Please install Node.js 18+"
    exit 1
fi

if ! command -v npm &> /dev/null; then
    echo "❌ npm not found. Please install npm"
    exit 1
fi

echo "✅ All prerequisites installed"
echo ""

# Setup Backend
echo "🔧 Setting up Backend..."
cd backend

echo "  → Restoring NuGet packages..."
dotnet restore

echo "  → Creating database..."
dotnet ef database update

echo "✅ Backend setup complete"
echo ""
cd ..

# Setup Frontend
echo "🔧 Setting up Frontend..."
cd frontend

echo "  → Installing npm packages..."
npm install

# Create .env.local if it doesn't exist
if [ ! -f .env.local ]; then
    echo "  → Creating .env.local..."
    cp .env.example .env.local
    echo "    ⚠️  Update .env.local with your actual API URL"
fi

echo "✅ Frontend setup complete"
echo ""
cd ..

echo "================================================"
echo "✅ Project initialization complete!"
echo ""
echo "📖 Next Steps:"
echo "1. Update backend/.env with OpenAI API key"
echo "2. Update frontend/.env.local with API URL"
echo ""
echo "🚀 To run the project:"
echo "   - Backend:  cd backend && dotnet run"
echo "   - Frontend: cd frontend && npm start"
echo ""
echo "📚 See QUICKSTART.md for more details"
