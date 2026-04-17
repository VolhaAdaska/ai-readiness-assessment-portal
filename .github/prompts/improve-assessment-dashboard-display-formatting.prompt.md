---
agent: 'agent'
description: Improve dashboard display formatting for organization and readiness level
---

Improve the dashboard card display formatting in the frontend for AI Readiness Assessment Portal.

Current issue:
- organization is shown as a GUID instead of a human-readable name
- readiness level is displayed in an awkward way such as "Level null"
- the dashboard feels too technical instead of product-like

Goal:
Make dashboard cards look more user-friendly and professional without redesigning the entire page.

Requirements:
- follow repository-wide and path-specific instructions
- keep the UI clean, modern, and enterprise-oriented
- keep the existing dashboard structure and layout
- use organizationName when available instead of organizationId
- format readiness level in a human-readable way
- handle missing readiness level gracefully for in-progress assessments
- before applying changes, list:
  1. files to create or update
  2. the display rules that will be changed
  3. how missing values will be shown

Expected display behavior:
- show organization name instead of GUID when available
- if organization name is unavailable, show a graceful fallback
- show readiness level labels such as:
  - Not assessed yet
  - Low
  - Emerging
  - Moderate
  - High
  - Advanced
- do not show raw values like "Level null"
- keep score/progress display intact
- keep action buttons intact

Constraints:
- do not redesign the full dashboard
- do not add unnecessary libraries
- keep formatting logic simple and reusable
- if useful, add a small helper/formatter for readiness labels

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code changes