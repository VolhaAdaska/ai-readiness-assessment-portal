---
agent: 'agent'
description: Implement domain tests for the MVP
---

Implement Domain layer tests for AI Readiness Assessment Portal.

Goal:
Add meaningful unit tests for the Domain model and business rules.

Requirements:
- follow repository-wide and path-specific instructions
- use xUnit and FluentAssertions
- keep tests clean, readable, and production-like
- focus on business behavior, not implementation trivia
- do not add unnecessary test infrastructure
- before applying changes, list:
  1. files to create or update
  2. which domain rules will be covered
  3. any assumptions needed

Expected test coverage:
- creating a new assessment
- starting an assessment
- category update behavior
- completion rules
- assessment cannot complete unless all required categories are complete
- recommendations can only be added after assessment is completed
- readiness score / readiness level behavior if it exists in Domain
- any important guard clauses or invariants

Constraints:
- keep one test class per domain concept where practical
- use descriptive test names
- cover normal and edge cases
- do not change production code unless a very small fix is required for testability

Return:
1. files to create or update
2. short purpose of each file
3. then generate the test code