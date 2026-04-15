# AI Readiness Assessment Portal - Copilot Instructions

## Project overview
This repository contains a full-stack AI Readiness Assessment Portal.
It helps assess organizational readiness for AI adoption across data, technology, processes, security, governance, and team capabilities.

## Architecture
- Domain contains business rules and readiness concepts.
- Application contains use cases, validation, and orchestration.
- Infrastructure contains EF Core persistence and integrations.
- Api exposes REST endpoints.
- Frontend is implemented separately in the frontend folder.

## Quality rules
- Keep endpoints thin.
- Keep scoring and readiness logic out of controllers.
- Prefer explicit names over generic helpers.
- Add tests for scoring and classification rules.
- Avoid placeholder implementations in committed code.

## UI rules
- The UI should look modern, clean, and professional.
- Prefer dashboards, score cards, category breakdowns, and clear forms.