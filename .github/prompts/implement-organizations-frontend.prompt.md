---
agent: 'agent'
description: Implement frontend support for organizations
---

Implement frontend support for organizations in AI Readiness Assessment Portal.

Goal:
Users should be able to:
- open an Organizations page from the main navigation
- see a list of organizations
- add a new organization using a simple form

Requirements:
- follow repository-wide and path-specific instructions
- keep the UI clean, modern, and enterprise-oriented
- use the existing frontend structure and fetch-based API client
- keep the implementation simple and production-like
- do not redesign the whole application
- before applying changes, list:
  1. files to create or update
  2. API calls needed
  3. UI sections to add

Expected frontend functionality:
- add Organizations button to the main navigation
- add Organizations page
- load organization list from backend
- show empty state if no organizations exist
- add a simple Add Organization form
- refresh the list after successful creation

Expected minimum organization fields in UI:
- name

Optional only if already easy to support:
- industry
- region

Constraints:
- do not add heavy state management
- do not add unnecessary UI libraries
- keep forms simple
- keep components reusable where it makes sense

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code changes