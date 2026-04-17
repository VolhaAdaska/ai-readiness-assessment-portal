---
agent: 'agent'
description: Implement backend support for organizations
---

Implement backend support for organizations in AI Readiness Assessment Portal.

Goal:
Users should be able to create organizations and list organizations.
Assessments should reference an existing organization instead of requiring users to type an organization GUID manually.

Requirements:
- follow repository-wide and path-specific instructions
- keep the implementation simple and production-like
- keep the current architecture intact
- add only the minimum functionality needed for the MVP
- do not redesign unrelated parts of the application
- before applying changes, list:
  1. files to create or update
  2. backend use cases to add
  3. API endpoints to expose

Expected backend functionality:
- create organization
- list organizations
- get organization by id only if needed for the MVP
- add persistence for organizations
- make sure assessments can reference organizations correctly

Expected minimum organization fields:
- id
- name

Optional only if already easy to support:
- industry
- region

Expected API endpoints:
- POST /api/organizations
- GET /api/organizations

Constraints:
- do not add unnecessary filters, pagination, or complex organization management
- keep DTOs minimal
- keep controllers thin
- keep business logic out of controllers

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code changes