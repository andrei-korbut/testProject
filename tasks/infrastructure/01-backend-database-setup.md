# 01 – Backend Project & Database Setup

## 📌 Priority
**CRITICAL** – Must be completed first before any other tasks

---

## 📝 Description

Set up the backend project with .NET 8 Web API and SQL Server database in Docker. This task ensures the project can run locally with migrations applied and a basic health check endpoint.

This is the foundational infrastructure task that enables all backend feature development.

---

## 🛠️ Technology Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| Backend Framework | .NET Web API | 8.0.x |
| Database | SQL Server (Docker) | 2022-latest |
| ORM | Entity Framework Core | 8.0.x |
| Container Orchestration | Docker Compose | Latest |
| Language | C# | 12.0 |

---

## 📋 Implementation Steps

### 1️⃣ Backend Project Setup

- Navigate to `backend/` folder
- Initialize .NET 8 Web API project:
  ```bash
  dotnet new webapi -n ProductManagementApi
  ```
- Install required NuGet packages:
  - `Microsoft.EntityFrameworkCore.SqlServer` (8.0.x)
  - `Microsoft.EntityFrameworkCore.Design` (8.0.x)
  - `Microsoft.EntityFrameworkCore.Tools` (8.0.x)
- Create `AppDbContext` class inheriting from `DbContext`
- Configure SQL Server connection string via environment variables
- Implement automatic migrations on startup: `db.Database.Migrate()` in `Program.cs`
- Set up basic logging and error handling middleware
- Create health check endpoint: `GET /health` → returns `{ "status": "healthy" }`

### 2️⃣ Database Configuration

- Use SQL Server Docker image: `mcr.microsoft.com/mssql/server:2022-latest`
- Configure environment variables:
  - `ACCEPT_EULA=Y`
  - `SA_PASSWORD` (strong password, min 8 chars with complexity)
- Expose port `1433` for local development access
- Configure Docker volume `sqlserver_data` for data persistence
- No initial tables required (migrations will handle Product entity in future tasks)

### 3️⃣ Docker Setup

Create `Dockerfile` in backend folder:
- Base image: `mcr.microsoft.com/dotnet/aspnet:8.0`
- Build image: `mcr.microsoft.com/dotnet/sdk:8.0`
- Expose port `80` (internal)
- Set environment variables for connection strings

Create/Update `docker-compose.yml` in project root:
- **backend** service:
  - Build from `./backend`
  - Port mapping: `5000:80`
  - Environment variables for DB connection
  - Depends on `database` service
  - Wait for database to be ready before starting
- **database** service:
  - Image: `mcr.microsoft.com/mssql/server:2022-latest`
  - Port mapping: `1433:1433`
  - Volume: `sqlserver_data:/var/opt/mssql`
  - Environment variables for SQL Server

---

## 🌐 Endpoints & URLs

### Backend Health Check
- **Endpoint:** `GET /health`
- **Response:** 200 OK
  ```json
  {
    "status": "healthy",
    "timestamp": "2026-02-17T10:30:00Z"
  }
  ```

### Docker URLs
- **Backend (External):** `http://localhost:5000`
- **Backend (Internal):** `http://backend:80`
- **Database (Internal):** `Server=database,1433;Database=ProductManagementDb;`
- **Database (External):** `localhost,1433`

---

## 🔗 Dependencies

**Prerequisites:**
- Docker Desktop installed and running
- .NET 8 SDK installed
- Git configured

**No prior tasks required** – This is the first task

---

## ✅ Acceptance Criteria

- [ ] Backend project created with .NET 8 Web API template
- [ ] `AppDbContext` class configured with SQL Server connection
- [ ] Entity Framework Core packages installed
- [ ] Automatic migrations execute on container startup
- [ ] `GET /health` endpoint returns 200 OK
- [ ] `Dockerfile` for backend builds successfully
- [ ] `docker-compose.yml` defines both backend and database services
- [ ] Docker containers start without errors: `docker-compose up -d`
- [ ] Backend logs show successful database connection
- [ ] Can access `http://localhost:5000/health` and receive healthy response
- [ ] Database container persists data using Docker volume
- [ ] Backend waits for database to be ready before applying migrations
- [ ] Build completes without warnings
- [ ] Project follows .NET project structure conventions

---

## 🧪 Testing & Validation

### Manual Testing Steps:
1. Run `docker-compose up --build`
2. Wait for containers to start (check logs)
3. Verify backend logs show: "Database migration completed"
4. Open browser to `http://localhost:5000/health`
5. Confirm response shows `{"status": "healthy"}`
6. Connect to SQL Server at `localhost,1433` with SA credentials
7. Verify database `ProductManagementDb` exists
8. Stop containers: `docker-compose down`
9. Restart: `docker-compose up -d`
10. Verify data persistence and health check still works

### Expected Behavior:
- Backend container starts within 30 seconds
- Database container accepts connections
- Health check responds in < 500ms
- No errors in container logs
- Migrations table exists in database

---

## 📌 Notes

- Use strong password for SA_PASSWORD (store in `.env` file, not committed to Git)
- Add `.env` to `.gitignore`
- Backend should retry database connection if not immediately available
- Consider adding health check to database service in docker-compose
- Future tasks will add Product entity and migrations

---

## 🚫 Out of Scope

- Authentication/Authorization
- HTTPS/SSL configuration
- Production deployment configuration
- Logging to external services
- Monitoring/Metrics
- API documentation (Swagger) - can be added later
- CORS configuration - will be added in frontend integration task
