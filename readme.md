# Monada Service Template

> A reusable .NET 10 Clean Architecture template for building Monada microservices and other service-based applications with a consistent, production-minded foundation.

**Author:** Mr.SMIT  
**GitHub:** [@SSMIT008](https://github.com/SSMIT008)  
**Repository:** [Monada_Service.Template](https://github.com/SSMIT008/Monada_Service.Template)  
**License:** MIT

---

## What this template creates

Create a new service with:

```powershell
dotnet new monada-service -n Monada.Realtime
```

The generated service follows this structure:

```text
Monada.Realtime/
├── readme.md
├── Monada.Realtime.API/
├── Monada.Realtime.Application/
├── Monada.Realtime.Domain/
└── Monada.Realtime.Infrastructure/
```

| Project | Responsibility | Depends On |
|---|---|---|
| **MonadaService.API** | HTTP API, endpoints/controllers, Swagger, startup and composition root | Application, Infrastructure |
| **MonadaService.Application** | Use cases, orchestration, application services, validation and behaviors | Domain |
| **MonadaService.Domain** | Entities, value objects, enums, domain rules and core abstractions | None |
| **MonadaService.Infrastructure** | Persistence, repositories, integrations, external services and technical implementations | Application, Domain |

### Architecture rule

**Domain depends on nothing.** Dependencies point inward toward the application and domain core. Infrastructure implements technical concerns; API composes and exposes the service.

---

# Quick Start

## Prerequisites

Check your .NET SDK:

```powershell
dotnet --version
```

This template targets:

```text
.NET 10
```

## 1. Create a GitHub Personal Access Token

For installing the package from GitHub Packages, create a GitHub Personal Access Token with:

```text
read:packages
```

Keep the token private. Do not commit it to source control or paste it into documentation.

GitHub token page:

https://github.com/settings/tokens/new

## 2. Add the GitHub Packages source

### PowerShell

```powershell
$env:GITHUB_PACKAGES_TOKEN = "YOUR_PERSONAL_ACCESS_TOKEN"

dotnet nuget add source "https://nuget.pkg.github.com/SSMIT008/index.json" `
  --name github-ssmit008 `
  --username YOUR_GITHUB_USERNAME `
  --password $env:GITHUB_PACKAGES_TOKEN
```

Verify the source:

```powershell
dotnet nuget list source
```

Expected source:

```text
github-ssmit008
https://nuget.pkg.github.com/SSMIT008/index.json
```

> On Windows, NuGet can store credentials using the platform-supported credential protection mechanism. Avoid `--store-password-in-clear-text` unless your environment specifically requires it.

## 3. Install the template

```powershell
dotnet new install Monada.Service.Template@1.0.0 `
  --nuget-source "https://nuget.pkg.github.com/SSMIT008/index.json"
```

Verify installation:

```powershell
dotnet new list
```

You should see:

```text
Monada Service - Clean Architecture    monada-service    [C#]    Web/Clean Architecture/API/Microservice
```

## 4. Create a new service

Move to the folder where you want the service to live:

```powershell
cd "D:\SMIT Studio\Dev\Projects_Production\Monada"
```

Create a service:

```powershell
dotnet new monada-service -n Monada.Realtime
```

Or use the template for another project:

```powershell
dotnet new monada-service -n MyCompany.Payments
```

The placeholder `MonadaService` in the template is replaced with the name supplied through `-n`.

## 5. Build the generated service

```powershell
cd Monada.Realtime\Monada.Realtime.API

dotnet restore
dotnet build
dotnet watch --no-hot-reload
```

## 6. Run the API

```powershell
dotnet watch --no-hot-reload
```

The template is HTTPS-first for local development. The default API endpoint is:

```text
https://localhost:8000
```

Swagger UI:

```text
https://localhost:8000/swagger
```

> When multiple generated services run at the same time, assign each service its own HTTPS port.

---

# Template Management

## List installed templates

```powershell
dotnet new list
```

## Reinstall or update the template

```powershell
dotnet new install Monada.Service.Template@1.0.0 `
  --nuget-source "https://nuget.pkg.github.com/SSMIT008/index.json" `
  --force
```

## Uninstall the template

```powershell
dotnet new uninstall Monada.Service.Template
```

## List NuGet sources

```powershell
dotnet nuget list source
```

## Remove the GitHub Packages source

```powershell
dotnet nuget remove source github-ssmit008
```

---

# What is included

The template provides a reusable baseline with:

- .NET 10
- Clean Architecture project separation
- API, Application, Domain and Infrastructure projects
- preconfigured project references
- Dependency Injection extension methods
- Global Usings
- Swagger / OpenAPI documentation
- HTTPS-first local launch profile
- root and generated-project documentation
- MIT license metadata
- NuGet template packaging
- GitHub Actions package publishing
- tag-driven package versioning

Current package choices include:

```text
Microsoft.AspNetCore.OpenApi                  10.0.10
Swashbuckle.AspNetCore                       10.2.3
Microsoft.Extensions.DependencyInjection.Abstractions  10.0.10
Microsoft.Extensions.Configuration.Abstractions       10.0.10
```

Swashbuckle provides the interactive Swagger UI used by the template. Microsoft OpenAPI support is retained as part of the .NET 10 API baseline.

---

# Dependency Injection

Each technical layer owns its own registration method while Domain stays framework-independent.

Generated `Program.cs` follows the composition pattern:

```csharp
using MonadaService.API;
using MonadaService.Application;
using MonadaService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

app.Run();
```

Domain intentionally has no `DependencyInjection.cs` because it should not depend on framework infrastructure.

---

# Swagger

The API layer registers Swagger through its own Dependency Injection extension.

```csharp
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MonadaService API",
        Version = "v1",
        Description = "Clean Architecture API service generated from Monada.Service.Template."
    });
});
```

Swagger is exposed only in Development by default:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

This gives generated services useful API documentation without exposing the interactive documentation automatically in production.

---

# Repository Structure

```text
Monada_Service.Template/
├── .github/
│   └── workflows/
│       └── publish.yml
├── docs/
│   ├── index.html
│   ├── styles.css
│   └── script.js
├── working/
│   └── content/
│       └── MonadaService/
│           ├── .template.config/
│           │   └── template.json
│           ├── MonadaService.API/
│           ├── MonadaService.Application/
│           ├── MonadaService.Domain/
│           ├── MonadaService.Infrastructure/
│           ├── .gitignore
│           └── readme.md
├── .gitignore
├── LICENSE
├── README.md
└── Template.csproj
```

`working/content/MonadaService` is the template source used when packaging and generating new services.
The `working` and `content` directories are packaging structure only. They are not included in generated services.
The `docs` directory contains the public GitHub Pages website for the template.

---

# Local Template Development

## Build the NuGet package

From the repository root:

```powershell
dotnet pack --configuration Release
```

The package is created under:

```text
bin/Release/
```

## Install the locally built package

```powershell
dotnet new install ".\bin\Release\Monada.Service.Template.1.0.0.nupkg"
```

## Generate a local test service

```powershell
mkdir TemplateTest
cd TemplateTest

dotnet new monada-service -n TestService
```

Then verify:

```powershell
cd TestService

dotnet restore
dotnet build
dotnet watch --no-hot-reload
```

## Remove the test installation

```powershell
dotnet new uninstall Monada.Service.Template
```

---

# Publishing a New Version

Publishing is automated through GitHub Actions.

The workflow is triggered when a Git tag beginning with `v` is pushed:

```text
v1.0.0
v1.0.1
v1.1.0
v2.0.0
```

The workflow removes the leading `v` and uses the remaining value as the NuGet package version.

## Release flow

Make your changes, then:

```powershell
git status
git add .
git commit -m "Improve Monada service template"
git push origin main
```

Create a release tag:

```powershell
git tag v1.1.0
git push origin v1.1.0
```

GitHub Actions will then:

1. check out the tagged source;
2. configure .NET 10;
3. derive the package version from the Git tag;
4. build the template package in Release mode;
5. publish the `.nupkg` to GitHub Packages.

Verify the release in:

```text
GitHub - Actions - Publish Template
GitHub - Packages - Monada.Service.Template
```

> Published package versions are immutable. Release a new version instead of trying to replace an existing package version.

---

# Creating the Architecture Manually

The template exists so this setup does not need to be repeated, but these are the core commands behind it.

## Create projects

```powershell
dotnet new web -n "MonadaService.API"
dotnet new classlib -n "MonadaService.Application"
dotnet new classlib -n "MonadaService.Domain"
dotnet new classlib -n "MonadaService.Infrastructure"
```

## Configure project references

### API

```powershell
cd MonadaService.API

dotnet add reference "..\MonadaService.Application\MonadaService.Application.csproj"
dotnet add reference "..\MonadaService.Infrastructure\MonadaService.Infrastructure.csproj"
```

### Application

```powershell
cd ..\MonadaService.Application

dotnet add reference "..\MonadaService.Domain\MonadaService.Domain.csproj"
```

### Infrastructure

```powershell
cd ..\MonadaService.Infrastructure

dotnet add reference "..\MonadaService.Application\MonadaService.Application.csproj"
dotnet add reference "..\MonadaService.Domain\MonadaService.Domain.csproj"
```

### Domain

```text
No project references.
```

## Add Swagger packages to API

```powershell
dotnet add package Microsoft.AspNetCore.OpenApi --version 10.0.10
dotnet add package Swashbuckle.AspNetCore --version 10.2.3
```

## Add DI abstractions where needed

```powershell
dotnet add package Microsoft.Extensions.DependencyInjection.Abstractions --version 10.0.10
dotnet add package Microsoft.Extensions.Configuration.Abstractions --version 10.0.10
```

---

# Design Principles

### Domain independence

> Domain depends on nothing.

Domain models and rules remain independent from HTTP, databases, hosting frameworks and external infrastructure.

### Separation of responsibilities

> Each layer has one architectural purpose.

API exposes the application. Application coordinates use cases. Domain expresses business concepts and rules. Infrastructure implements technical details.

### Explicit dependencies

> Project references document architecture.

A dependency should exist because the layer genuinely needs it, not because it is convenient.

### Reuse without premature microservices

> A template makes services easy to create; it does not mean every feature should become a service.

Create a new service when a responsibility forms a meaningful boundary and the separation provides real architectural value.

### Build with intent

> The goal is not complexity. The goal is a foundation that is understood, intentional, reusable and worth being proud of.

---

# Monada

This template was created while evolving **Monada**, a product designed as:

> **A hidden digital sanctuary composed of Rooms.**

The template gives future Monada services a consistent architectural starting point while allowing the product roadmap to determine when a service boundary is actually needed.

Potential service boundaries include areas such as Identity, Realtime, Media and Notifications, but each should be introduced only when its responsibility justifies independent evolution.

---

## License

MIT License. See [LICENSE](LICENSE).