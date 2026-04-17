---
agent: 'agent'
description: Implement backend support for assessment dashboard listing with progress
---

Implement backend support for the assessment dashboard in AI Readiness Assessment Portal.

Current state:
- creating an assessment works
- assessment details retrieval works
- category assessment updates work
- completion flow exists
- there is currently no proper dashboard listing for assessments

Goal:
Add backend functionality so the frontend dashboard can show all created assessments with enough information to continue working on them.

Requirements:
- follow repository-wide and path-specific instructions
- keep the implementation simple and production-like
- keep the current architecture intact
- add only the minimum functionality needed for the MVP dashboard
- do not redesign existing features
- before applying changes, list:
  1. files to create or update
  2. the new query/use case to add
  3. the API endpoint to expose
  4. the fields that will be returned for the dashboard

Expected behavior:
- add a query/use case for listing assessments
- add an API endpoint for listing assessments
- each assessment item should include enough data for dashboard display and continue-work flow

Expected minimum response fields per assessment item:
- assessmentId
- organizationId
- status
- createdAt
- startedAt
- completedAt
- readinessLevel if available
- overallScore if available
- totalCategories
- completedCategories
- completionPercentage

Progress calculation rules:
- totalCategories should reflect the number of assessment categories
- completedCategories should reflect how many categories are considered completed
- completionPercentage should be derived from completedCategories / totalCategories
- keep progress logic practical and aligned with the current domain model

Constraints:
- do not add reporting/history beyond this dashboard need
- do not add unnecessary filters or pagination unless already needed
- do not introduce unnecessary abstractions
- return only the fields needed for the dashboard

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code changes