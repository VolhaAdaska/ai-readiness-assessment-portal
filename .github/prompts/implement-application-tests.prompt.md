---
agent: 'agent'
description: Implement application tests for the MVP
---

Implement Application layer tests for AI Readiness Assessment Portal.

Goal:
Add meaningful tests for the MVP application use cases.

Requirements:
- follow repository-wide and path-specific instructions
- use xUnit and FluentAssertions
- keep tests clean and production-like
- test handler behavior and orchestration logic
- mock or fake dependencies only where needed
- do not add unnecessary test complexity
- before applying changes, list:
  1. files to create or update
  2. which handlers/use cases will be tested
  3. what dependencies will be mocked or faked

Expected test coverage:
- CreateInitialAssessmentCommandHandler
- StartInitialAssessmentCommandHandler
- UpdateCategoryAssessmentCommandHandler
- CompleteInitialAssessmentCommandHandler
- GetInitialAssessmentQueryHandler
- GetCategoryAssessmentQueryHandler
- dashboard/listing query if implemented
- organization creation/listing use cases if implemented

Test expectations:
- successful flows
- not found cases where relevant
- validation / conflict behavior where relevant
- completion flow generates recommendations in the correct order
- progress/listing mapping returns expected values

Constraints:
- keep tests focused on application behavior
- do not test EF Core internals
- do not test controller plumbing here
- do not change production code unless a very small fix is required for testability

Return:
1. files to create or update
2. short purpose of each file
3. then generate the test code