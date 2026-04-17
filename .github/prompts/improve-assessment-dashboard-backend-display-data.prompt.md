---
agent: 'agent'
description: Improve backend dashboard data for human-readable assessment display
---

Improve the backend assessment dashboard data in AI Readiness Assessment Portal so the frontend can display human-readable information.

Current issue:
- dashboard assessment cards show organization GUIDs instead of organization names
- readiness level values are not presented cleanly in the frontend because the backend data is not ideal for display

Goal:
Return cleaner dashboard data for display without changing the core architecture.

Requirements:
- follow repository-wide and path-specific instructions
- keep the implementation simple and production-like
- keep the current architecture intact
- update only what is needed for the MVP dashboard display
- do not redesign unrelated features
- before applying changes, list:
  1. files to create or update
  2. backend response fields to improve
  3. whether any query joins or mapping changes are needed

Expected backend improvements:
- assessment dashboard/list items should include organizationName
- keep organizationId if it is still useful internally
- readinessLevel should be returned in a stable way that frontend can format cleanly
- if readiness level is missing for in-progress assessments, make that state explicit and predictable

Expected minimum response fields per dashboard item:
- assessmentId
- organizationId
- organizationName
- status
- createdAt
- startedAt
- completedAt
- readinessLevel
- overallScore
- totalCategories
- completedCategories
- completionPercentage

Constraints:
- do not add unnecessary filters or pagination
- do not redesign the domain model unless a tiny safe improvement is required
- keep controllers thin
- keep mapping logic clean

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code changes