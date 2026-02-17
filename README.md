# Product Management System

A full-stack product management system built with .NET 8 Web API backend and React frontend, using SQL Server database in Docker containers.

## Prerequisites

- Docker Desktop installed and running
- .NET 8 SDK (for local development)
- Node.js 20.x LTS (for frontend development, when implemented)

## Project Structure

```
testProject/
├── backend/
│   ├── ProductManagementApi.sln # Visual Studio solution file
│   ├── ProductManagementApi/    # .NET 8 Web API project
│   │   ├── Data/               # DbContext and data access
│   │   ├── Migrations/         # EF Core migrations
│   │   └── Program.cs          # Application entry point
│   └── Dockerfile              # Backend container configuration
├── frontend/                    # React frontend (to be implemented)
├── docs/                       # Project documentation
├── tasks/                      # Task specifications
├── docker-compose.yml          # Multi-container Docker configuration
├── .env                        # Environment variables (not committed)
└── .env.example                # Environment variables template
```

## Getting Started

### 1. Environment Configuration

First, ensure your `.env` file exists with proper configuration:

```bash
# Copy the example file if .env doesn't exist
cp .env.example .env
```

The `.env` file contains:
- `SA_PASSWORD`: SQL Server SA account password (must be strong: 12+ chars, mixed case, numbers, symbols)
- `DB_PASSWORD`: Same as SA_PASSWORD for the application
- `ACCEPT_EULA`: Set to `Y` to accept SQL Server EULA
- `MSSQL_PID`: SQL Server edition (`Developer` for development)

### 2. Start Docker Desktop

Ensure Docker Desktop is running before proceeding.

### 3. Build and Run

Build and start all containers:

```bash
docker-compose up --build
```

Or run in detached mode (background):

```bash
docker-compose up --build -d
```

The backend API will be available at: `http://localhost:5000`

### 4. Verify Setup

Check that everything is working:

```bash
# Health check endpoint
curl http://localhost:5000/health

# Expected response
{"status":"healthy","timestamp":"2026-02-17T10:30:00Z"}
```

Access Swagger UI for API documentation:
```
http://localhost:5000/swagger
```

## Available Services

| Service | External URL | Internal URL | Container Name |
|---------|--------------|--------------|----------------|
| Backend API | http://localhost:5000 | http://backend:80 | productmanagement-backend |
| SQL Server | localhost,1433 | database,1433 | productmanagement-db |
| Swagger UI | http://localhost:5000/swagger | - | - |

## Database Connection

Connect to SQL Server using any SQL client:

- **Server**: `localhost,1433`
- **User**: `sa`
- **Password**: Value from `SA_PASSWORD` in `.env` file
- **Database**: `ProductManagementDb`
- **Trust Server Certificate**: Yes

## Docker Commands

```bash
# Start containers
docker-compose up -d

# Stop containers
docker-compose down

# View logs
docker-compose logs -f

# View backend logs only
docker-compose logs -f backend

# Rebuild containers
docker-compose up --build

# Stop and remove volumes (deletes database data)
docker-compose down -v
```

## Development Workflow

### Local Development with Visual Studio

1. Open the solution file: `backend/ProductManagementApi.sln`

2. Ensure SQL Server is running in Docker:
   ```bash
   docker-compose up -d database
   ```

3. Press **F5** to run the backend API with debugging

The API will launch with Swagger UI automatically opening in your browser.

### Local Development (Without Docker - Command Line)

1. Ensure SQL Server is running in Docker:
   ```bash
   docker-compose up -d database
   ```

2. Run the backend locally:
   ```bash
   cd backend/ProductManagementApi
   dotnet run
   ```

Or build and run the entire solution:
```bash
cd backend
dotnet build ProductManagementApi.sln
dotnet run --project ProductManagementApi
```

### Database Migrations

Create a new migration:
```bash
cd backend/ProductManagementApi
dotnet ef migrations add MigrationName
```

Apply migrations manually:
```bash
dotnet ef database update
```

**Note**: Migrations are automatically applied when the backend container starts.

## API Endpoints

### Health Check
- **GET** `/health`
  - Returns: `{ "status": "healthy", "timestamp": "..." }`
  - Status: 200 OK (healthy) or 503 Service Unavailable (unhealthy)

## Technology Stack

### Backend
- .NET 8.0 Web API
- Entity Framework Core 8.0.11
- SQL Server 2022
- Swagger/OpenAPI

### Infrastructure
- Docker & Docker Compose
- SQL Server in Docker container
- Multi-stage Docker builds

## Troubleshooting

### Docker Desktop Not Running
**Error**: `The system cannot find the file specified` or `pipe/dockerDesktopLinuxEngine`

**Solution**: Start Docker Desktop and wait for it to fully initialize.

### Database Connection Failed
**Error**: Backend logs show database connection errors

**Solutions**:
1. Check that `.env` file exists and contains valid passwords
2. Ensure SQL Server container is healthy: `docker-compose ps`
3. Verify password strength (SQL Server requires strong passwords)
4. Wait 30 seconds for SQL Server to fully start

### Port Already in Use
**Error**: `Bind for 0.0.0.0:5000 failed: port is already allocated`

**Solution**: Stop the service using the port or change the port in `docker-compose.yml`

### Build Errors
**Error**: Backend container fails to build

**Solutions**:
1. Verify .NET 8 SDK is available in build container
2. Check that all NuGet packages are compatible with .NET 8
3. Run `docker-compose build --no-cache` to rebuild from scratch

## Project Status

✅ **Completed**:
- Backend infrastructure setup
- SQL Server database in Docker
- Entity Framework Core with migrations
- Health check endpoint
- Docker Compose configuration
- Swagger/OpenAPI documentation

🚧 **Pending**:
- Frontend setup (React + TypeScript)
- Product entity and CRUD operations
- Additional API endpoints
- CORS configuration
- Authentication/Authorization

## Next Steps

1. Implement Product entity and CRUD endpoints (backend tasks 01-05)
2. Set up React frontend (infrastructure task 02)
3. Implement frontend UI components (frontend tasks 01-04)
4. Add authentication and authorization
5. Production deployment configuration

## License

[Specify your license here]

## Contributors

[Add contributor information]
