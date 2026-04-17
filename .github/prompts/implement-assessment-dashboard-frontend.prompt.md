---
agent: 'agent'
description: Implement frontend dashboard listing with progress and continue-work flow
---

Implement the assessment dashboard in the frontend for AI Readiness Assessment Portal.

Current state:
- frontend shell exists
- assessment creation works
- assessment details and category editing flows exist
- there is currently no proper dashboard listing of created assessments

Goal:
Show created assessments on the dashboard so users can see progress and continue working on an assessment.

Requirements:
- follow repository-wide and path-specific instructions
- keep the UI clean, modern, and enterprise-oriented
- use the existing frontend architecture and fetch-based API client
- keep the implementation simple and production-like
- do not redesign the whole application
- before applying changes, list:
  1. files to create or update
  2. the API call(s) needed
  3. the UI sections to add

Expected dashboard behavior:
- load created assessments from the backend
- show each assessment in a clear card or row
- display:
  - assessment id or short id
  - organization id
  - status
  - created date
  - readiness level if available
  - overall score if available
  - progress percentage
  - completed categories out of total categories
- provide a clear action button:
  - Continue Assessment if not completed
  - View Summary if completed
- clicking the action should navigate to the correct page

Expected UI additions:
- dashboard list section
- progress indicator (text and/or progress bar)
- empty state when there are no assessments
- loading state
- error state

Constraints:
- do not add heavy state management
- do not add unnecessary UI libraries
- keep components reusable where it makes sense
- keep the dashboard focused on MVP needs only

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code changes