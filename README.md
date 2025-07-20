# Flight Board System

A real-time flight information display system that provides departure and arrival information for airports. The system features a modern web-based interface with live updates and comprehensive flight tracking capabilities.

## 🚀 Features

- Real-time flight departure and arrival information
- Interactive flight board display
- Flight search and filtering capabilities
- Responsive design for desktop and mobile devices
- Real-time updates using WebSocket connections
- Flight status tracking (On Time, Delayed, Cancelled, Boarding)

## 🏗️ Architecture

### Backend Architecture
The backend is built using a **RESTful API architecture** with the following key components:

- **API Server**: Handles HTTP requests and WebSocket connections for real-time updates
- **Database Layer**: Stores flight information, schedules, and historical data
- **External API Integration**: Connects to aviation data providers for real-time flight information
- **WebSocket Service**: Provides real-time updates to connected clients

**Technology Stack (Backend):**
- ASP.NET Core Web API (C#)
- Database: SQLite with Entity Framework Core
- Real-time Communication: SignalR
- Testing Framework: xUnit with Moq for mocking

### Frontend Architecture
The frontend follows a **Component-Based Architecture** using modern JavaScript frameworks:

- **Component Structure**: Modular, reusable UI components
- **State Management**: Centralized state for flight data and application state
- **Real-time Updates**: WebSocket integration for live flight information

**Technology Stack (Frontend):**
- React with TypeScript
- State Management: Redux Toolkit & TanStack Query (React Query)
- Styling: styled-components
- Build Tool: Vite
- Real-time: SignalR Client

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- **.NET 6 SDK** (or later) - [Download here](https://dotnet.microsoft.com/download)
- **Node.js** (v18.0 or higher) - [Download here](https://nodejs.org/)
- **npm** or **yarn** package manager
- **Git** for version control

## 🛠️ Installation & Setup

### Backend Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/AhuvaElkarif/FlightBoardSystem.git
   cd FlightBoardSystem
   ```

2. **Navigate to backend directory**
   ```bash
   cd backend
   # or cd FlightBoard.API (depending on your folder structure)
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Update database**
   ```bash
   # Apply Entity Framework migrations
   dotnet ef database update
   
   # If migrations don't exist, create them first
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Build the application**
   ```bash
   dotnet build
   ```

6. **Start the backend server**
   ```bash
   # Development mode with hot reload
   dotnet watch run
   
   # Production mode
   dotnet run
   ```

   The backend server will run on `https://localhost:7207` (HTTPS) and `http://localhost:5207` (HTTP)

### Frontend Setup

1. **Navigate to frontend directory**
   ```bash
   cd ../frontend
   # or cd ../client (depending on your folder structure)
   ```

2. **Install dependencies**
   ```bash
   npm install
   # or yarn install
   ```

3. **Environment Configuration**
   ```bash
   # Create environment file
   cp .env.example .env.local
   
   # Edit environment variables
   nano .env.local
   ```

   **Required Environment Variables:**
   ```env
   # API Configuration
   REACT_APP_API_URL=https://localhost:7207/api
   REACT_APP_SIGNALR_URL=https://localhost:7207/flighthub
   ```

4. **Start the frontend development server**
   ```bash
   npm start
   # or yarn start
   ```

   The frontend application will run on `http://localhost:5173` (or next available port)

## 🚦 Running the Application

### Development Mode
1. **Start Backend**: In the backend directory, run `dotnet watch run`
2. **Start Frontend**: In the frontend directory, run `npm start`
3. **Access Application**: Open `http://localhost:5173` in your browser

### Production Mode
1. **Build Frontend**:
   ```bash
   cd frontend
   npm run build
   ```

2. **Start Backend**:
   ```bash
   cd ../backend
   dotnet run --configuration Release
   ```

3. **Access Application**: Open `https://localhost:7207` in your browser

## 📚 Third-Party Libraries

### Backend Dependencies
- **ASP.NET Core** - Web API framework for .NET
- **Entity Framework Core** - Object-Relational Mapping (ORM)
- **Microsoft.EntityFrameworkCore.Sqlite** - SQLite database provider
- **SignalR** - Real-time communication library
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Validation library for .NET
- **xUnit** - Testing framework
- **Moq** - Mocking framework for unit tests
- **Microsoft.Extensions.Logging** - Structured logging

### Frontend Dependencies
- **React** - Frontend library
- **TypeScript** - Type-safe JavaScript
- **Redux Toolkit** - State management
- **TanStack Query (React Query)** - Server state management and caching
- **@microsoft/signalr** - SignalR client for real-time communication
- **React Hook Form** - Form handling and validation
- **React Router** - Client-side routing
- **Axios** - HTTP client for API calls

### Development Dependencies
- **Vite** - Build tool and development server
- **ESLint** - Code linting
- **Prettier** - Code formatting
- **Jest** - Testing framework
- **React Testing Library** - React component testing
- **MSW (Mock Service Worker)** - API mocking for tests

## 🧪 Testing

### Backend Tests
```bash
cd backend
dotnet test                           # Run all tests
dotnet test --logger trx             # Run tests with detailed output
dotnet test --collect:"XPlat Code Coverage"  # Run tests with coverage
```

## 📁 Project Structure

```
FlightBoardSystem/
├── backend/
│   ├── FlightBoard.API/              # Web API project
│   │   ├── Controllers/              # API controllers
│   │   ├── Hubs/                     # SignalR hubs
│   │   ├── Program.cs                # Application entry point
│   │   └── appsettings.json          # Configuration
│   ├── FlightBoard.Application/      # Application layer
│   │   ├── DTOs/                     # Data Transfer Objects
│   │   ├── Services/                 # Business logic services
│   │   ├── Validators/               # FluentValidation rules
│   │   └── Interfaces/               # Service interfaces
│   ├── FlightBoard.Domain/           # Domain layer
│   │   ├── Entities/                 # Domain entities
│   │   ├── Enums/                    # Enumerations
│   │   └── Interfaces/               # Repository interfaces
│   ├── FlightBoard.Infrastructure/   # Infrastructure layer
│   │   ├── Data/                     # DbContext and configurations
│   │   ├── Repositories/             # Repository implementations
│   │   └── Migrations/               # EF Core migrations
│   └── FlightBoard.Tests/            # Unit tests
│       ├── Controllers/              # Controller tests
│       ├── Services/                 # Service tests
│       └── Validators/               # Validation tests
├── frontend/
│   ├── src/
│   │   ├── components/               # React components
│   │   ├── pages/                    # Page components
│   │   ├── services/                 # API services
│   │   ├── store/                    # Redux store configuration
│   │   ├── hooks/                    # Custom hooks
│   │   ├── types/                    # TypeScript type definitions
│   │   ├── utils/                    # Utility functions
│   │   └── styles/                   # CSS/SCSS files
│   ├── public/                       # Static files
│   ├── tests/                        # Frontend tests
│   ├── package.json
│   └── .env.example
└── .gitignore
```

## 🔧 API Documentation

### Base URL
- Development: `https://localhost:7202/api`

### Main Endpoints

#### Flights
- `GET /api/flights` - Get all flights with calculated status
- `POST /api/flights` - Create new flight (with validation)
- `DELETE /api/flights/{id}` - Delete flight by ID
- `GET /api/flights/search?status={status}&destination={destination}` - Search flights by status and/or destination

#### Real-time Updates
- SignalR Hub: `https://localhost:7202/flighthub`
- Events: `FlightAdded`, `FlightDeleted`

### Flight Status Calculation
The system automatically calculates flight status based on current server time:
- **Scheduled**: More than 30 minutes before departure
- **Boarding**: From 30 minutes before departure until departure time
- **Departed**: From departure time until 60 minutes after
- **Landed**: More than 60 minutes after departure time

## 🚨 Troubleshooting

### Common Issues

1. **Port already in use**
   ```bash
   # Find and kill process using port 7207 (Windows)
   netstat -ano | findstr :7207
   taskkill /PID <PID> /F
   
   # Find and kill process using port 7207 (macOS/Linux)
   lsof -ti:7207 | xargs kill -9
   ```

2. **Database issues**
   ```bash
   # Reset database
   dotnet ef database drop
   dotnet ef database update
   
   # Recreate migrations
   dotnet ef migrations remove
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

3. **CORS errors**
   - Verify frontend URL is added to CORS policy in backend
   - Check API URL configuration in frontend .env file

4. **SignalR connection failed**
   - Ensure backend SignalR hub is running
   - Check firewall/proxy settings
   - Verify SignalR URL in frontend configuration
   - For HTTPS issues, accept the development certificate: `dotnet dev-certs https --trust`

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📧 Contact

**Author**: Ahuva Elkarif  
**GitHub**: [@AhuvaElkarif](https://github.com/AhuvaElkarif)  
**Project Link**: [https://github.com/AhuvaElkarif/FlightBoardSystem](https://github.com/AhuvaElkarif/FlightBoardSystem)

---

## 🎥 Demo Video

Attach Demo Video

---

*Last updated: July 2025*