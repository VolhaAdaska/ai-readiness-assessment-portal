---
agent: 'agent'
description: Fix assessment completion flow so recommendations are added in the correct order
---

Fix the backend error that happens when completing an assessment.

Current issue:
- clicking "Complete Assessment" causes a backend error
- the Domain model allows AddRecommendation only when assessment status is Completed
- the completion flow likely generates or adds recommendations before the assessment status is changed to Completed

Goal:
Make the completion flow work correctly without breaking domain rules.

Requirements:
- inspect the completion flow across Domain and Application
- identify the exact order-of-operations issue
- preserve the current architecture and domain rules if possible
- prefer fixing the completion orchestration/order instead of weakening domain invariants
- keep the solution production-like and minimal
- do not redesign unrelated parts of the app
- before applying changes, list:
  1. the root cause
  2. the files to update
  3. the exact order that should happen during completion

Expected correct completion flow:
- validate all required categories are complete
- calculate score and readiness level
- transition assessment status to Completed
- then add generated recommendations

Return:
1. root cause
2. files to update
3. summary of the fix strategy
4. then generate the code changes