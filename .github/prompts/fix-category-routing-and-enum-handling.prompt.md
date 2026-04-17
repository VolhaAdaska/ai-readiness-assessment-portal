---
agent: 'agent'
description: Fix category routing and enum handling between frontend and backend
---

Fix the category assessment flow in AI Readiness Assessment Portal.

Current issue:
- assessment creation works
- assessment details page loads categories
- category cards currently display numeric enum values like 0, 1, 2, 3, 4, 5
- clicking "Assess" leads to a frontend error: "Category not found"

Goal:
Make category navigation and category assessment pages work correctly.

Expected behavior:
- category cards should display human-readable category names
- clicking "Assess" should navigate to the correct category assessment page
- the category assessment page should load the selected category correctly
- frontend and backend should agree on category identifiers

Requirements:
- inspect frontend routing, frontend category display logic, API client usage, and backend category route expectations
- determine whether category values should be passed as enum names or numeric values
- choose one consistent approach and apply it across the frontend
- keep the backend contracts intact unless a tiny safe improvement is truly required
- do not redesign the application
- keep the fix minimal and production-like
- before applying changes, list:
  1. the root cause of the mismatch
  2. the files to update
  3. the chosen category identifier strategy

Likely areas to inspect:
- assessment details page
- category assessment page
- route params handling
- frontend type definitions
- API client methods for getCategory and updateCategory
- any enum mapping helpers

Return:
1. root cause
2. files to update
3. summary of the fix strategy
4. then generate the code changes