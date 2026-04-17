---
agent: 'agent'
description: Implement the Create Assessment page for the MVP
---

Implement the Create Assessment page for the AI Readiness Assessment Portal MVP.

Context:
- The frontend shell already exists.
- The backend API already supports assessment creation.
- The frontend uses React, TypeScript, Vite, and a fetch-based API client.
- The UI should remain clean, modern, and enterprise-oriented.

Requirements:
- create a real Create Assessment page
- include only the minimum fields needed for the MVP
- use the existing fetch-based API client
- include loading state and error state
- after successful creation, navigate to the assessment details page
- do not introduce unnecessary form libraries unless already present
- do not redesign the entire app shell
- keep the implementation simple and production-like
- before applying changes, list the files to create or update

Expected minimum fields:
- organizationId
- assessment name

Additional guidance:
- if the backend uses slightly different field names, align the frontend to the existing API contracts
- keep validation lightweight and practical
- keep components reusable where it makes sense
- do not add unnecessary dependencies

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code