---
agent: 'agent'
description: Implement the application layer for the MVP
---

Implement the Application layer for the initial assessment MVP in AI Readiness Assessment Portal.

Context:
We already have the Domain model and a reviewed Application layer design.
The product should support the following MVP workflow:
- create an assessment
- start an assessment
- update a category assessment
- complete an assessment
- retrieve assessment details
- retrieve category assessment details

Requirements:
- follow repository-wide and path-specific instructions
- keep the design simple, clean, and production-like
- use explicit names
- create only the minimum necessary files for the MVP
- include commands, queries, request/response DTOs, and validation
- keep business rules in Domain/Application, not in API
- do not implement API controllers yet
- do not implement EF Core persistence yet
- do not introduce unnecessary abstractions
- keep one type per file where practical
- before applying changes, list the files to create or update

Expected MVP use cases:
Commands:
- CreateInitialAssessmentCommand
- StartInitialAssessmentCommand
- UpdateCategoryAssessmentCommand
- CompleteInitialAssessmentCommand

Queries:
- GetInitialAssessmentQuery
- GetCategoryAssessmentQuery

Expected deliverables:
- command files
- query files
- request and response DTOs
- validators
- application-level orchestration for the MVP
- correct namespaces and folder structure inside the Application project

Constraints:
- no repositories or persistence implementations yet
- no API contracts yet
- no frontend code yet
- no post-MVP features such as list/history/reporting workflows
- recommendations may be produced as part of assessment completion flow

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code