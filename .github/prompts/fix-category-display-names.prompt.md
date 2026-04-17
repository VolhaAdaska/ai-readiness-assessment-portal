---
agent: 'agent'
description: Fix category display names in the frontend
---

Fix the category display in the frontend so category enum values are shown as human-readable names instead of numeric values.

Current issue:
- the UI displays category values like 0, 1, 2, 3, 4, 5
- this affects assessment details, category score summaries, and any category-related UI

Goal:
Display readable category names throughout the frontend.

Expected category labels:
- Data
- Process
- Technology
- Security
- Governance
- Team

Requirements:
- inspect the existing frontend category types and rendering logic
- add a clean reusable mapping helper or formatter
- apply it consistently wherever categories are displayed
- keep the fix simple and production-like
- do not redesign the application
- before applying changes, list:
  1. the files to update
  2. where raw enum values are currently rendered
  3. how the display mapping will be applied

Return:
1. files to update
2. short purpose of each file
3. then generate the code changes