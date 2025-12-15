# DailyExchangeRate

## A small .NET backend + worker and a React frontend that imports and exposes daily exchange rates from the NBP API

### Repository layout
- `API` — solution containing API and Worker (.NET 10)
- `Frontend` — Vite + React frontend application (TypeScript)

### API solution items
- `DailyExchangeRate.slnx` — solution containing API and Worker projects
- `DailyExchangeRate.API` — ASP.NET Core Web API
- `DailyExchangeRate.Application` — application layer (DTOs, services, mappers)
- `DailyExchangeRate.Domain` — domain entities and models
- `DailyExchangeRate.Infrastructure` — EF Core `DbContext`, migrations and repositories
- `DailyExchangeRate.Worker` — background worker that imports exchange rates from NBP and saves them in the database

### Prerequisites
- .NET 10 SDK (or newer)
- Node.js 18 (or newer)
- SQL Server (local or remote)
- (Optional) `dotnet-ef` tools for applying migrations manually: `dotnet tool install --global dotnet-ef`

### Quick start

1) Build the solution

```bash
dotnet build src/API/DailyExchangeRate.slnx
```

2) Run the API

```powershell
dotnet run --project src/API/DailyExchangeRate.API/DailyExchangeRate.API.csproj
```

The API configuration files are in `src/API/DailyExchangeRate.API/` (`appsettings.json`, `appsettings.Development.json`). By default the API listens on `http://localhost:5118` (see launch settings).

3) Run the worker

```powershell
dotnet run --project src/API/DailyExchangeRate.Worker/DailyExchangeRate.Worker.csproj
```

The worker configuration files are in `src/API/DailyExchangeRate.Worker/` (`appsettings.json`, `appsettings.Development.json`). The import job is scheduled via Quartz (cron in `NbpRatesImportJobCron`) and runs once on startup.

4) Run the frontend

```powershell
cd src/Frontend
Copy-Item .env.example .env
npm install
npm run dev
```

On Unix/macOS you can use `cp .env.example .env` to create .env file.

### Environment / Configuration
- `VITE_API_URL` — used by the frontend (example in `src/Frontend/.env.example`): `VITE_API_URL=http://localhost:5118/api`
- `ConnectionStrings:DefaultConnection` — update in `src/API/DailyExchangeRate.API/appsettings.json` and `src/API/DailyExchangeRate.Worker/appsettings.json` to point to your SQL Server.

### Database migrations
- Migrations live under `src/API/DailyExchangeRate.Infrastructure/Migrations`.
- Migrations are applied automatically on API and Worker startup (`db.Database.Migrate()`).
- To apply migrations manually with EF CLI:

```powershell
dotnet tool install --global dotnet-ef --version 10.*
dotnet ef database update --project src/API/DailyExchangeRate.Infrastructure/DailyExchangeRate.Infrastructure.csproj --startup-project src/API/DailyExchangeRate.API/DailyExchangeRate.API.csproj
```

### Notes & suggestions
- Ensure the SQL Server instance from the connection string is accessible before running the apps.
- Frontend expects the API base URL in `VITE_API_URL` and will call `/exchangeRate` endpoint.