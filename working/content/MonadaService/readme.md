# MonadaService - Clean Architecture

> Generated service foundation built with .NET 10 and Clean Architecture.

This project was created from **Monada.Service.Template** and provides a consistent starting point for independently evolving services.

## Project Structure

```text
MonadaService/
├── MonadaService.API/
├── MonadaService.Application/
├── MonadaService.Domain/
└── MonadaService.Infrastructure/
```

| Project | Responsibility | Depends On |
|---|---|---|
| **MonadaService.API** | HTTP API, endpoints/controllers, Swagger, startup and composition root | Application, Infrastructure |
| **MonadaService.Application** | Use cases, orchestration, application services, validation and behaviors | Domain |
| **MonadaService.Domain** | Entities, value objects, enums, domain rules and core abstractions | None |
| **MonadaService.Infrastructure** | Persistence, repositories, integrations, external services and technical implementations | Application, Domain |

## Architecture Principles

> **Domain depends on nothing.**

The Domain layer stays independent from HTTP, databases and infrastructure frameworks. Application coordinates use cases around the domain. Infrastructure implements technical concerns. API exposes and composes the service.

> **A service boundary should represent a meaningful responsibility.**

The existence of a reusable microservice template does not mean every feature must become a separate service.

## Build

From the generated service root:

```powershell
dotnet restore
dotnet build
```

## Run

```powershell
cd MonadaService.API

dotnet restore
dotnet build
dotnet watch --no-hot-reload
```

Default local endpoint:

```text
https://localhost:8000
```

Swagger UI:

```text
https://localhost:8000/swagger
```

When multiple services run together, give each service its own HTTPS port.

## Verify Project References

```powershell
dotnet list MonadaService.API reference
dotnet list MonadaService.Application reference
dotnet list MonadaService.Infrastructure reference
```

Expected dependency model:

```text
API
├── Application
└── Infrastructure

Application
└── Domain

Infrastructure
├── Application
└── Domain

Domain
└── None
```

## Dependency Injection

Service registration is organized by layer:

```csharp
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);
```

Domain intentionally has no DI registration because it remains framework-independent.

## Swagger

Swagger is enabled in Development and is available at:

```text
/swagger
```

The generated API metadata uses the service name automatically because `MonadaService` is the template replacement token.

## Next Steps

Add only the capabilities this service genuinely owns. Typical additions may include:

- application commands and queries;
- validation and pipeline behaviors;
- persistence and repositories;
- authentication/authorization when required;
- messaging or caching when required;
- health checks and observability;
- tests and CI/CD specific to the service.

Keep the architecture intentional: dependencies should be explicit, responsibilities should be clear, and Domain should remain independent.

---

Generated with **Monada.Service.Template** by Mr. SMIT.