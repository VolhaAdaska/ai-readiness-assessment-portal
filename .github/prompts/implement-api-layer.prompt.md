---
agent: 'agent'
description: Implement the API layer for the MVP
---

Implement the API layer for the initial assessment MVP in AI Readiness Assessment Portal.

Context:
We already have the Domain, Application, and Infrastructure layers.
The API should expose the minimum necessary endpoints for the MVP and stay thin.

The MVP workflow should support:
- creating an assessment
- starting an assessment
- updating a category assessment
- completing an assessment
- retrieving assessment details
- retrieving category assessment details

Requirements:
- follow repository-wide and path-specific instructions
- keep controllers thin
- keep business logic out of controllers
- use DTOs and request models appropriate for a React frontend
- include validation handling
- use clear REST-style endpoint naming
- do not implement frontend yet
- do not add unnecessary endpoints
- before applying changes, list the files to create or update

Expected scope:
- controllers
- API request models if needed
- API response models if needed
- mapping from API contracts to Application layer requests
- dependency injection wiring if API needs updates
- Swagger/OpenAPI should continue to work

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code