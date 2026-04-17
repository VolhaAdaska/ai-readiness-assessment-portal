---
agent: 'agent'
description: Align frontend API client with the Assessments backend controller
---

Align the frontend API client with the existing AssessmentsController backend API for local development.

Backend context:
The existing ASP.NET Core controller is:

- [Route("api/[controller]")] on AssessmentsController
- effective base route: /api/assessments

Implemented endpoints:
- POST /api/assessments
- POST /api/assessments/{id}/start
- PUT /api/assessments/{id}/categories/{category}
- POST /api/assessments/{id}/complete
- GET /api/assessments/{id}
- GET /api/assessments/{id}/categories/{category}

Frontend context:
The frontend has a client.ts file with fetch-based API calls.
It currently uses API_BASE_URL and may not yet be aligned with the backend routing and local dev setup.

Goals:
- make the frontend client match the actual backend routes exactly
- make local frontend-backend integration work reliably during development
- keep the current architecture and MVP scope intact

Requirements:
- inspect the existing frontend client and backend controller
- update the frontend API client to align with the real backend routes
- prefer a clean local development setup using either:
  1. Vite proxy with relative /api routes
  or
  2. a correct VITE_API_BASE_URL setup
- choose the simpler and more robust option for the current project
- ensure the category route works correctly with the backend CategoryType parameter
- verify that request/response types are aligned with the actual controller contracts
- do not redesign the app
- do not add unnecessary libraries
- before applying changes, list:
  1. the route mismatches or integration issues found
  2. the files to update
  3. the chosen local integration strategy

Likely files to inspect/update:
- frontend/src/services/client.ts
- frontend/src/types/assessment.ts
- frontend/vite.config.ts
- frontend/.env if needed

Return:
1. detected mismatches
2. files to update
3. summary of the integration approach
4. then generate the code changes