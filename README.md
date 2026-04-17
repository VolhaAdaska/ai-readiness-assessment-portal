# AI Readiness Assessment Portal

## Overview
AI Readiness Assessment Portal is a full-stack MVP for evaluating how prepared an organization is to adopt AI.

The product supports a structured baseline assessment across key readiness dimensions (Data, Technology, Process, Security, Governance, Team), scoring, category-level progress tracking, and recommendation generation. It also includes organization management and a dashboard to continue active assessments.

This repository is built as a practical assignment project with a clean/layered architecture and explicit backend/frontend separation.

## Features
### Assessment Management
- Create a baseline assessment for an organization
- Start an assessment workflow
- View full assessment details
- Edit category maturity and observations
- Complete an assessment and generate recommendations

### Dashboard
- List assessments with summary information
- Show status, progress percentage, category completion, readiness level, and overall score
- Continue in-progress assessments
- View summary for completed assessments

### Organization Management
- List organizations
- Create organizations (name-based creation)
- Use existing organizations in assessment creation flow

### Recommendation and Scoring Flow
- Rule-based recommendation generation during completion
- Category and overall readiness scoring surfaced through API and UI

## Architecture
The solution follows a layered structure:

- `Domain`: Core entities, enums, and business rules (assessment lifecycle, category behavior, recommendation model)
- `Application`: Use cases with commands/queries, DTOs, validation, and orchestration
- `Infrastructure`: EF Core persistence, repositories, and technical implementations (including recommendation generator implementation)
- `Api`: Thin ASP.NET Core controllers exposing REST endpoints
- `frontend`: React + TypeScript + Vite client app

Solution file:
- `AiReadinessAssessment.slnx`

## Tech Stack
### Backend
- ASP.NET Core Web API (`net10.0`)
- Clean/layered architecture
- EF Core (SQLite)
- OpenAPI in development

### Frontend
- React 19 + TypeScript
- Vite
- Tailwind CSS
- Fetch-based API client

### Testing
- xUnit
- FluentAssertions
- coverlet collector

## GitHub Copilot Customization
This repository includes Copilot customization artifacts to guide implementation quality and consistency:

- Repository-wide guidance: `.github/copilot-instructions.md`
- Path-specific instructions:
  - `.github/instructions/backend.instructions.md`
  - `.github/instructions/frontend.instructions.md`
  - `.github/instructions/tests.instructions.md`
- Agent workflow and quality guardrails: `.github/AGENTS.md`

These files enforce priorities such as thin controllers, explicit business rules, clean boundaries, polished UI standards, and test coverage for business logic.

## Prompt Files and Agent-Based Workflow
The repository contains reusable implementation prompts under `.github/prompts/` for major workstreams, including:

- Domain/Application/Infrastructure/API implementation
- Frontend shell and feature pages
- Dashboard and organizations flows
- Integration and bug-fix prompts
- Test implementation prompts

This supports an agent-style workflow where tasks are executed with explicit scope, constraints, and expected deliverables.

## How to Run the Backend
### Prerequisites
- .NET SDK 10.0 (project targets `net10.0`)

### From repository root
```bash
dotnet restore AiReadinessAssessment.slnx
dotnet build AiReadinessAssessment.slnx
```

### Run API
```bash
dotnet run --project src/AiReadinessAssessment.Api/AiReadinessAssessment.Api.csproj
```

Default launch URLs (from `launchSettings.json`):
- `https://localhost:7116`
- `http://localhost:5270`

OpenAPI is mapped in development via `app.MapOpenApi()`.

## How to Run the Frontend
### Prerequisites
- Node.js + npm

### From `frontend/`
```bash
npm install
npm run dev
```

The frontend uses Vite dev server and proxies `/api` to backend HTTPS (`https://localhost:7116`) via `frontend/vite.config.ts`.

## Database and Migrations
The backend uses SQLite with default connection string in `src/AiReadinessAssessment.Api/appsettings.json`:

- `Data Source=ai_readiness_assessment.db`

EF Core migrations are in:
- `src/AiReadinessAssessment.Infrastructure/Migrations`

### Apply migrations
```bash
dotnet ef database update --project src/AiReadinessAssessment.Infrastructure/AiReadinessAssessment.Infrastructure.csproj --startup-project src/AiReadinessAssessment.Api/AiReadinessAssessment.Api.csproj
```

### Create a new migration (when schema changes)
```bash
dotnet ef migrations add <MigrationName> --project src/AiReadinessAssessment.Infrastructure/AiReadinessAssessment.Infrastructure.csproj --startup-project src/AiReadinessAssessment.Api/AiReadinessAssessment.Api.csproj
```

## Tests
Test projects:
- `tests/AiReadinessAssessment.Domain.Tests`
- `tests/AiReadinessAssessment.Application.Tests`

Current coverage areas include:
- Domain behavior for baseline assessment, category assessment, recommendations
- Application command/query handlers for assessments and organizations

Run all tests from repository root:
```bash
dotnet test AiReadinessAssessment.slnx
```

## MVP Scope
Implemented MVP scope includes:
- Organization management (list/create)
- End-to-end initial assessment flow (create, start, assess categories, complete)
- Dashboard for assessment tracking and continuation
- Recommendation generation during completion
- Clean architecture layering and automated tests for core logic

The MVP intentionally focuses on baseline readiness workflow and does not yet include advanced enterprise capabilities such as authentication/authorization, multi-tenant permissions, or analytics reporting.

## Future Improvements
Potential next steps after MVP:
- Authentication and role-based authorization
- Advanced filtering/search on dashboard and organizations
- Richer recommendation explainability and prioritization controls
- Assessment templates and configurable category models
- Audit trails and change history
- CI pipeline integration with quality gates and test coverage thresholds
- Deployment-ready environment configuration and containerization
