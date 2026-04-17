---
agent: 'agent'
description: Update Create Assessment page to use organization selection
---

Update the Create Assessment page so users select an existing organization instead of typing an organization GUID manually.

Goal:
When creating an assessment, the user should:
- load organizations from the backend
- choose an organization from a dropdown or select control
- submit the selected organization's id behind the scenes

Requirements:
- follow repository-wide and path-specific instructions
- keep the UI clean and simple
- use the existing frontend API client
- preserve the current assessment creation flow
- include loading and error states for organizations
- before applying changes, list:
  1. files to create or update
  2. API calls needed
  3. UI changes to the form

Expected behavior:
- organizationId text input should be removed
- replace it with organization selection
- if there are no organizations, show a useful empty state and guide the user to create one
- after successful assessment creation, navigate to the assessment details page

Constraints:
- do not redesign unrelated pages
- do not add unnecessary dependencies
- keep validation practical and minimal

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code changes