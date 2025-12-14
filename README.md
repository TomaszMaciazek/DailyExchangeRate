# DailyExchangeRate

## A small .NET backend + worker and a React frontend application that imports and exposes daily exchange rates from NBP api

### Repository layout
- `API` — solution containing API and Worker projects
- `Frontend` — Vite + React frontend application

### API solution items
- `DailyExchangeRate.slnx` — solution containing API and Worker projects
- `DailyExchangeRate.API` — ASP.NET Core Web API
- `DailyExchangeRate.Application` — application layer containing Dto classes, services and mappers
- `DailyExchangeRate.Domain` — domain entities and models
- `DailyExchangeRate.Infrastructure` — infrastructure layer containing EF Core DbContext, migrations and repositories
- `DailyExchangeRate.Worker` — background worker that imports exchange rates from NBP site and saves them in the databas

### Prerequisites
- .NET 10
- Node.js 18+
- (Optional) `dotnet-ef` tools for applying migrations manually: `dotnet tool install --global dotnet-ef`

### Quick start

1) Build the solution

```bash
dotnet build src/API/DailyExchangeRate.slnx
```

2) Run the API

```bash
dotnet run --project src/API/DailyExchangeRate.API/DailyExchangeRate.API.csproj
```

The API default configuration files are in `src/API/DailyExchangeRate.API/` (`appsettings.json`, `appsettings.Development.json`).

3) Run the worker

```bash
dotnet run --project src/API/DailyExchangeRate.Worker/DailyExchangeRate.Worker.csproj
```

The worker default configuration files are in `src/API/DailyExchangeRate.Worker/` (`appsettings.json`, `appsettings.Development.json`).\
Exchange rates import job is configured via Quartz.NET library and NbpRatesImportJobCron configuration setting value to run every two hours and also one time after worker application startup.

4) Run the frontend

```bash
cd src/Frontend
cp .env.example .env
npm install
npm run dev
```

### Database migrations
- Migrations live under `src/API/DailyExchangeRate.Infrastructure/Migrations`.
- Migrations are applied during API and Worker applications startup
- To apply migrations using EF CLI (example):

1) Install dotnet-ef

```bash
dotnet tool install --global dotnet-ef --version 10.*
```

2) Run migrations

```bash
dotnet ef database update --project src/API/DailyExchangeRate.Infrastructure/DailyExchangeRate.Infrastructure.csproj --startup-project src/API/DailyExchangeRate.API/DailyExchangeRate.API.csproj
```