@echo off
REM Script to initialize the entire project on Windows

echo.
echo 🚀 E-Commerce AI Agent - Project Initialization
echo ================================================
echo.

REM Check prerequisites
echo 📋 Checking prerequisites...

where dotnet >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ❌ .NET SDK not found. Please install .NET 8.0
    exit /b 1
)

where node >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ❌ Node.js not found. Please install Node.js 18+
    exit /b 1
)

where npm >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ❌ npm not found. Please install npm
    exit /b 1
)

echo ✅ All prerequisites installed
echo.

REM Setup Backend
echo 🔧 Setting up Backend...
cd backend

echo   → Restoring NuGet packages...
call dotnet restore

echo   → Creating database...
call dotnet ef database update

echo ✅ Backend setup complete
echo.
cd ..

REM Setup Frontend
echo 🔧 Setting up Frontend...
cd frontend

echo   → Installing npm packages...
call npm install

REM Create .env.local if it doesn't exist
if not exist .env.local (
    echo   → Creating .env.local...
    copy .env.example .env.local
    echo     ⚠️  Update .env.local with your actual API URL
)

echo ✅ Frontend setup complete
echo.
cd ..

echo ================================================
echo ✅ Project initialization complete!
echo.
echo 📖 Next Steps:
echo 1. Update backend/.env with OpenAI API key
echo 2. Update frontend/.env.local with API URL
echo.
echo 🚀 To run the project:
echo    - Backend:  cd backend ^&^& dotnet run
echo    - Frontend: cd frontend ^&^& npm start
echo.
echo 📚 See QUICKSTART.md for more details
echo.
